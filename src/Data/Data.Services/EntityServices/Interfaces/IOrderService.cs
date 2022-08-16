namespace Data.Services.EntityServices.Interfaces
{
    using Data.Services.DtoModels;

    public interface IOrderService
    {
        public OrderDto GetOrder(string orderId);

        public void AddOrder(string userId, OrderDto orderDto);

        public void DeleteOrder(string orderId);
    }
}
