namespace GraphQLOrderExample.DomainModels;

public class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string OrderNumber { get; set; }

    public OrderState State { get; set; } = OrderState.Provisional;

    public Party Buyer { get; set; }
    public Party Supplier { get; set; }
    public Party PickupFrom { get; set; }
    public Party DeliveryTo { get; set; }
    

    public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}

public enum OrderState
{
    Cancelled = 0,
    Provisional = 1,
    Confirmed = 2,
    Shipped = 3,
    Delivered = 4
}