using GraphQLOrderExample.Data;
using GraphQLOrderExample.DomainModels;
using HotChocolate.Pagination;
using Microsoft.EntityFrameworkCore;
using PagingQueryableExtensions = HotChocolate.Data.PagingQueryableExtensions;

namespace GraphQLOrderExample.Services;

public sealed class OrderService(OrderContext context)
{
    public async Task<Order?> GetOrderByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        return await context.Orders.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
    
    public async Task<Page<Order>> GetOrdersAsync(
        PagingArguments pagingArguments,
        CancellationToken cancellationToken = default)
        => await PagingQueryableExtensions.ToPageAsync(context.Orders
                .AsNoTracking()
                .OrderBy(t => t.OrderNumber)
                .ThenBy(i => i.Id), pagingArguments, cancellationToken);
}

