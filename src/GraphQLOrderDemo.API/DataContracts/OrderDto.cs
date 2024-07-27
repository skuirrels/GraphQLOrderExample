using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderExample.DataContracts;

public class OrderDto
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string OrderNumber { get; set; }

    //public OrderState State { get; set; } = OrderState.Provisional;

    public PartyDto Buyer { get; set; }
    public PartyDto Supplier { get; set; }
    public PartyDto PickupFrom { get; set; }
    public PartyDto DeliveryTo { get; set; }

    public ICollection<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
}