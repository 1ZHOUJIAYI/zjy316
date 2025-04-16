public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task UpdateOrderAsync(int id, Order order);
    Task DeleteOrderAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
}