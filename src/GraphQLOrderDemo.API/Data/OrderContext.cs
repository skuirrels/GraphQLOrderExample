using GraphQLOrderExample.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace GraphQLOrderExample.Data;

public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderLine> OrderLines => Set<OrderLine>();
    public DbSet<Party> Parties => Set<Party>();
}