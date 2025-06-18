﻿namespace GraphQLOrderExample.DomainModels;

public class OrderLine
{
    public Guid Id { get; set; }
    public int LineNumber { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}