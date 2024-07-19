var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GraphQLOrderDemo_API>("GraphQLOrderDemoAPI");

builder.Build().Run();