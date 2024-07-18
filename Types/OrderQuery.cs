using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderExample.Types;

public class OrderQuery
{
    public List<Order> GetOrders() => DataSeeder.GenerateOrders(100);
}