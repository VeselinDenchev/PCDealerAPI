namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IOrderService
    {
        public OrderDto[] GetAccountOrders(string userName);

        public OrderDto GetOrder(string orderId);

        public void AddOrder(OrderDto orderDto, string userId);

        public void DeleteOrder(string orderId);
    }
}
