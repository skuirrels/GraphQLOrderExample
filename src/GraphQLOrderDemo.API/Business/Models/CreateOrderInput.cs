using GraphQLOrderExample.DomainModels;

namespace GraphQLOrderExample.Business.Models;

public record CreateOrderInput(
    string OrderNumber,
    CreatePartyInput Buyer,
    CreatePartyInput Supplier,
    CreatePartyInput PickupFrom,
    CreatePartyInput DeliveryTo,
    IEnumerable<CreateOrderLineInput>? OrderLines = null
);

public record CreatePartyInput(
    string Name,
    string Address,
    string City,
    string PostalCode,
    string Country,
    string Email,
    string Phone
);

public record CreateOrderLineInput(
    string ProductCode,
    string ProductDescription,
    int Quantity,
    decimal UnitPrice
);

public record UpdateOrderInput(
    string? OrderNumber = null,
    CreatePartyInput? Buyer = null,
    CreatePartyInput? Supplier = null,
    CreatePartyInput? PickupFrom = null,
    CreatePartyInput? DeliveryTo = null
);
