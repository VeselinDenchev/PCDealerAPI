namespace Data.Services.EntityServices
{
    using AutoMapper;

    using Data.DbContext;
    using Data.Models.Entities;
    using Data.Services.DtoModels;
    using Data.Services.EntityServices.Interfaces;

    public class OrderService : IOrderService
    {
        public OrderService(PcDealerDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public PcDealerDbContext DbContext { get; init; }

        public IMapper Mapper { get; init; }

        // TODO: Get account orders

        public OrderDto GetOrder(string orderId)
        {
            Order order = this.DbContext.Orders.Where(o => o.Id == orderId && o.IsDeleted == false).FirstOrDefault();
            OrderDto orderDto = this.Mapper.Map<Order, OrderDto>(order);

            return orderDto;
        }

        public void AddOrder(string userId, OrderDto orderDto)
        {
            // Check if user exists

            Order order = this.Mapper.Map<OrderDto, Order>(orderDto);

            this.DbContext.CartItems.AddRange(order.CartItems);
            this.DbContext.Orders.Add(order);
            //this.DbContext.Users.Where(u => u.Id == userId).First().Orders.Add(order);

            this.DbContext.SaveChanges();

            orderDto.CreatedAtUtc = order.CreatedAtUtc;
            orderDto.ModifiedAtUtc = order.ModifiedAtUtc;
        }


        public void DeleteOrder(string orderId)
        {
            bool exists = this.DbContext.Orders.Any(b => b.Id == orderId);
            if (!exists) throw new ArgumentException("Such order doesn't exist!");

            Order order = this.DbContext.Orders.Where(r => r.Id == orderId && r.IsDeleted == false).First();

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
