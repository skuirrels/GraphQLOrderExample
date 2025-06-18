using GraphQLOrderExample.Business.Models;
using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderExample.Business.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Order>> GetPagedOrdersAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<Order> CreateOrderAsync(CreateOrderInput input, CancellationToken cancellationToken = default);
    Task<Order> UpdateOrderAsync(Guid id, UpdateOrderInput input, CancellationToken cancellationToken = default);
    Task<Order> UpdateOrderStateAsync(Guid id, OrderState newState, CancellationToken cancellationToken = default);
    Task<bool> DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Order> AddOrderLineAsync(Guid orderId, CreateOrderLineInput input, CancellationToken cancellationToken = default);
    Task<Order> RemoveOrderLineAsync(Guid orderId, Guid orderLineId, CancellationToken cancellationToken = default);
}
