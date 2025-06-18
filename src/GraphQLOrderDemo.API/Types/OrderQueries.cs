using GraphQLOrderExample.Business.Interfaces;
using GraphQLOrderExample.Business.Models;
using GraphQLOrderExample.DomainModels;
using HotChocolate;

namespace GraphQLOrderExample.Types;

public class OrderQueries
{
    /// <summary>
    /// Gets all orders with optional filtering and sorting
    /// </summary>
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Order>> GetOrdersAsync(
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        return await orderService.GetOrdersAsync(cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of orders
    /// </summary>
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10, MaxPageSize = 50)]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Order>> GetPagedOrdersAsync(
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        var result = await orderService.GetOrdersAsync(cancellationToken);
        return result;
    }

    /// <summary>
    /// Gets a specific order by ID
    /// </summary>
    public async Task<Order?> GetOrderByIdAsync(
        Guid id,
        [Service] IOrderService orderService,
        CancellationToken cancellationToken)
    {
        return await orderService.GetOrderByIdAsync(id, cancellationToken);
    }
}
