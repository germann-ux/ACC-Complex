using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Projects;
using System; 

var builder = DistributedApplication.CreateBuilder(args);

// === Parámetros seguros === //
var sqlPassword = builder.AddParameter("sql-password", secret: true, value: "ACC-Complex-P@assword123!");

// === Script de creación para identidad === //
const string identitySchemaScript = """
   IF DB_ID('ACC_Identity') IS NULL
CREATE DATABASE ACC_Identity;
""";

// === Script de creación para académica === //
const string academicSchemaScript = """
    IF DB_ID('ACC_Academic') IS NULL
CREATE DATABASE ACC_Academic;
""";

// === Contenedor SQL para identidad === //
var sqlIdentity = builder.AddSqlServer("acc-sql-identity", sqlPassword, port: 1434)
    .WithContainerName("acc-sql-identity-container")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithVolume("volume-sql-identity", "/var/opt/mssql")
    .PublishAsConnectionString();

var dbIdentity = sqlIdentity.AddDatabase("acc-identity-db", "ACC_Identity")
    .WithCreationScript(identitySchemaScript);

// === Contenedor SQL para académica === //
var sqlAcademic = builder.AddSqlServer("acc-sql-academic", sqlPassword, port: 1435)
    .WithContainerName("acc-sql-academic-container")
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithVolume("volume-sql-academic", "/var/opt/mssql")
    .PublishAsConnectionString();

var dbAcademic = sqlAcademic.AddDatabase("acc-academic-db", "ACC_Academic")
    .WithCreationScript(academicSchemaScript);

// === Redis === //
var redis = builder.AddRedis("acc-redis")
    .WithContainerName("acc-redis-container");

// === Servicios === //
var compilerApi = builder.AddProject<Projects.ACC_Compiler>("acc-compiler")
    .WithReference(redis);

var accApi = builder.AddProject<Projects.ACC_API>("acc-api")
    .WithReference(dbAcademic)
    .WaitFor(dbAcademic);

var webApp = builder.AddProject<Projects.ACC_WebApp>("acc-blazor")
    .WithReference(dbIdentity)
    .WaitFor(dbIdentity)
    .WithReference(dbAcademic)
    .WaitFor(dbAcademic)
    .WithReference(redis);

builder.Build().Run();
