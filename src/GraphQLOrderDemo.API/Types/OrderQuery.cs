using GraphQLOrderEExample.Data;
using GraphQLOrderExample.Data;
using GraphQLOrderExample.DataContracts;
using GraphQLOrderExample.DomainModels;
using GraphQLOrderExample.DomainServices;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrderExample.Types;

[QueryType]
public static class OrderQuery
{
    public static async Task<Order?> GetOrderById(
        Guid id,
        OrderService orderService,
        CancellationToken cancellationToken) 
        => await orderService.GetOrderByIdAsync(id, cancellationToken);
    
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 20, MaxPageSize = 50)]
    public static async Task<Connection<Order>> GetOrdersAsync(
        PagingArguments pagingArguments,
        OrderService orderService,
        CancellationToken cancellationToken) 
        => await orderService.GetOrdersAsync(pagingArguments, cancellationToken).ToConnectionAsync();
    
}