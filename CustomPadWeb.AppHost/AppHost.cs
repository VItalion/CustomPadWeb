var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.CustomPadWeb_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.CustomPadWeb_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.CustomPadWeb_AuthService>("custompadweb-authservice");

builder.Build().Run();
