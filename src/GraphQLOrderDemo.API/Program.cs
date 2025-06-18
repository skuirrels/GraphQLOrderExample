using GraphQLOrderExample;
using GraphQLOrderExample.Business.Interfaces;
using GraphQLOrderExample.Business.Services;
using GraphQLOrderExample.Data;
using GraphQLOrderExample.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Use Docker ConnectionString Param or fallback to the connnectionstring is appsettings.json
var connectionString = Environment.GetEnvironmentVariable("APP_CONNECTIONSTRING")??
                       builder.Configuration.GetConnectionString("OrdersDB");

builder.Services
    .AddDbContext<OrderContext>(
        o => o.UseNpgsql(connectionString));

// Used for the migrations
builder.Services.AddTransient<OrderContext>();

// Register business layer services
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<OrderQueries>()
    .AddMutationType<OrderMutations>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddInstrumentation();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGraphQL();

MigrateDatabase(app);

app.Run();

static void MigrateDatabase(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<OrderContext>();
        db.Database.Migrate();
        db.Seed();
    }
}
