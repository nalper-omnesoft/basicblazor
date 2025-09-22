var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("postgres").WithPgWeb();

// This creates the database "app_db" automatically
var postgresdb = postgres.AddDatabase("appdb");

// The creation script runs INSIDE the app_db database
var creationScript = """
                                          CREATE DATABASE appdb;
                     """;

                   

postgresdb.WithCreationScript(creationScript);


var apiService = builder.AddProject<Projects.DevOpsAssignment_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.DevOpsAssignment_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();