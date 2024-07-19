using GraphQLOrderEExample.Data;

namespace GraphQLOrderExample.Data;

public static class OrderContextExtensions
{
    public static void Seed(this OrderContext context)
    {
        if (!context.Orders.Any())
        {
            var orders = DataSeeder.GenerateOrders(1000);
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }

    }
}