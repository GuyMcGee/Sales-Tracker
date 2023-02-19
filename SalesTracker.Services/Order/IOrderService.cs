public interface IOrderService
{
    Task<OrderDetails> CreateOrder(OrderCreate orderCreate);
    Task<List<OrderListItem>> GetOrders();
    Task<OrderDetails> GetOrderById(int id);
    Task<bool> DeleteOrder(int id);
    Task<OrderDetails> EditOrder(int id, OrderEdit updateorder);
}   