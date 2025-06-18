var builder = DistributedApplication.CreateBuilder(args);

// Add PostgreSQL container
var postgres = builder.AddPostgres("postgres");

// Add the database
var ordersDb = postgres.AddDatabase("OrdersDB");

// Add the API project with database reference
builder.AddProject<Projects.GraphQLOrderDemo_API>("GraphQLOrderDemoAPI")
    .WithReference(ordersDb);

builder.Build().Run();