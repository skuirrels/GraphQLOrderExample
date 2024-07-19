using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderEExample.Data;

public class DataSeeder
{
    private static Random _random = new Random();

    public static List<Order> GenerateOrders(int count)
    {
        var orders = new List<Order>();
        for (int i = 0; i < count; i++)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = GenerateOrderNumber(),
                State = (OrderState)_random.Next(0, 4),
                Buyer = GenerateParty(PartyType.Buyer),
                Supplier = GenerateParty(PartyType.Supplier),
                PickupFrom = GenerateParty(PartyType.Warehouse),
                DeliveryTo = GenerateParty(PartyType.Carrier),
                OrderLines = GenerateOrderLines()
            };
            orders.Add(order);
        }
        return orders;
    }

    private static string GenerateOrderNumber()
    {
        return $"ORD-{_random.Next(1000, 9999)}";
    }

    private static Party GenerateParty(PartyType type)
    {
        return new Party
        {
            Id = Guid.NewGuid(),
            Name = GenerateRandomString(10),
            Address = GenerateRandomString(15),
            City = GenerateRandomString(7),
            PostalCode = GenerateRandomString(5),
            Country = GenerateRandomString(7),
            Email = GenerateRandomString(5) + "@example.com",
            Phone = GenerateRandomPhoneNumber(),
            Type = type
        };
    }

    private static List<OrderLine> GenerateOrderLines()
    {
        var orderLines = new List<OrderLine>();
        int lineCount = _random.Next(1, 5);
        for (int i = 0; i < lineCount; i++)
        {
            var orderLine = new OrderLine
            {
                Id = Guid.NewGuid(),
                LineNumber = i + 1,
                ProductCode = GenerateRandomString(8),
                ProductDescription = GenerateRandomString(20),
                Quantity = _random.Next(1, 100)
            };
            orderLines.Add(orderLine);
        }
        return orderLines;
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[_random.Next(chars.Length)];
        }

        return new string(stringChars);
    }

    private static string GenerateRandomPhoneNumber()
    {
        return _random.Next(100, 999).ToString() + "-" +
               _random.Next(100, 999).ToString() + "-" +
               _random.Next(1000, 9999).ToString();
    }
}