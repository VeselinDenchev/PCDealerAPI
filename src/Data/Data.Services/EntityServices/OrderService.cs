namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        public OrderService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        public OrderDto[] GetAccountOrders(string userName)
        {
            Order[] orders = this.DbContext.Orders.Where(o => o.User.UserName == userName && o.IsDeleted == false)
                                                    .Include(o => o.User)
                                                    .Include(o => o.CartItems).ThenInclude(ci => ci.Product).ThenInclude(p => p.Images)
                                                    .ToArray();

            OrderDto[] orderDtos =  this.Mapper.Map<Order[], OrderDto[]>(orders);

            return orderDtos;
        }

        public OrderDto GetOrder(string orderId)
        {
            Order order = this.DbContext.Orders.Where(o => o.Id == orderId && o.IsDeleted == false).FirstOrDefault();
            OrderDto orderDto = this.Mapper.Map<Order, OrderDto>(order);

            return orderDto;
        }

        public void AddOrder(OrderDto orderDto, string userName)
        {
            List<Product> buyedProducts = new List<Product>();

            foreach (CartItemDto cartItem in orderDto.CartItems)
            {
                bool productExists = this.DbContext.Products.AsNoTracking()
                                                            .Any(p => p.Id == cartItem.ProductId && p.IsDeleted == false);
                if (!productExists) throw new ArgumentException("Such product doesn't exist!");

                short desiredQuantity = cartItem.Quantity;
                short quantityInStock = this.DbContext.Products.Where(p => p.Id == cartItem.ProductId)
                                                                .AsNoTracking()
                                                                .FirstOrDefault().Quantity;

                bool isEnoughQuantityInStock = desiredQuantity <= quantityInStock;
                if (!isEnoughQuantityInStock) throw new ArgumentOutOfRangeException("Not enough quantity ot this product in stock!");

                Product buyedProduct = this.DbContext.Products.Where(p => p.Id == cartItem.ProductId).AsNoTracking().First();
                buyedProduct.Quantity = (short)(quantityInStock - desiredQuantity);
                buyedProducts.Add(buyedProduct);
            }

            orderDto.User = this.DbContext.Users.Where(u => u.UserName == userName).AsNoTracking().First();

            Order order = this.Mapper.Map<OrderDto, Order>(orderDto);

            this.DbContext.CartItems.AddRange(order.CartItems);

            this.DbContext.Orders.Add(order);

            this.DbContext.Entry(order.User).State = EntityState.Detached;

            this.DbContext.Products.UpdateRange(buyedProducts);

            this.DbContext.SaveChanges();

            orderDto.CreatedAtUtc = DateTime.UtcNow;
            orderDto.ModifiedAtUtc = DateTime.UtcNow;
            foreach (CartItemDto cartItem in orderDto.CartItems)
            {
                cartItem.CreatedAtUtc = orderDto.CreatedAtUtc;
                cartItem.ModifiedAtUtc = orderDto.ModifiedAtUtc;
            }
        }


        public void DeleteOrder(string orderId)
        {
            bool exists = this.DbContext.Orders.Any(b => b.Id == orderId);
            if (!exists) throw new ArgumentException("Such order doesn't exist!");

            Order order = this.DbContext.Orders.Where(r => r.Id == orderId && r.IsDeleted == false)
                                                .Include(o => o.CartItems)
                                                .First();

            foreach (CartItem cartItem in order.CartItems)
            {
                cartItem.IsDeleted = true;
                cartItem.DeletedAtUtc = DateTime.UtcNow;
            }

            order.IsDeleted = true;
            order.DeletedAtUtc = DateTime.UtcNow;

            this.DbContext.CartItems.UpdateRange(order.CartItems);
            this.DbContext.Orders.Update(order);

            this.DbContext.SaveChanges();
        }
    }
}
