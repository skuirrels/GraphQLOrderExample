namespace GraphQLOrderExample.DomainModels;

public class Party
{
    public Guid Id { get; init; }

    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public PartyType Type { get; set; }
}

public enum PartyType
{
    Unknown = 0,
    Buyer = 1,
    Supplier = 2,
    Carrier = 3,
    Warehouse = 4
}