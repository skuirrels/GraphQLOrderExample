using GraphQLOrderExample.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<OrderQuery>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
