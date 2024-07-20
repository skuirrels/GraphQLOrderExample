using GraphQLOrderEExample.Data;
using GraphQLOrderExample.Data;
using GraphQLOrderExample.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrderExample.Types;

public class OrderQuery
{
    [UseProjection]
    public IQueryable<Order> GetOrders(OrderContext context) =>
        context.Orders.AsNoTracking();
    
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10, MaxPageSize = 50)]
    [UseProjection]
    public IQueryable<Order> GetPagedOrders(OrderContext context) =>
        context.Orders.AsNoTracking();
    
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Order> GetOrderById(Guid id, OrderContext context) =>
        context.Orders.Where(o => o.Id == id);
}