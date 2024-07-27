namespace GraphQLOrderExample.DataContracts;

public class OrderLineDto
{
    public Guid Id { get; set; }
    public int LineNumber { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public int Quantity { get; set; }

    public Guid OrderId { get; set; }

}