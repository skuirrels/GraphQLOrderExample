using GraphQLOrderEExample.Data;
using GraphQLOrderExample.Data;
using GraphQLOrderExample.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrderExample.Types;

public class OrderQuery
{
    public IQueryable<Order> GetOrders(OrderContext context) =>
        context.Orders.Include(o=>o.OrderLines).Include(b => b.Buyer).Include(s => s.Supplier).Include(p => p.PickupFrom).Include(d => d.DeliveryTo);
}