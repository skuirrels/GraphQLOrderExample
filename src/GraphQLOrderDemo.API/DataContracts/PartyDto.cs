namespace GraphQLOrderExample.DataContracts;

public class PartyDto
{
    public Guid Id { get; init; }

    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    //public PartyType Type { get; set; }
}