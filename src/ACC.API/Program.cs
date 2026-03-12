using ACC.API.Extensions;
using ACC.API.Interfaces;
using ACC.API.Services;
using ACC.Data;
using ACC.ExternalClients.Extensions;
using ACC.ServiceDefaults;
using ACC.Shared.Core;
using ACC.Shared.Interfaces;
using ACC.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var tlsTerminatedAtProxy = builder.Configuration.GetValue("Network:TlsTerminatedAtProxy", false);
var applyMigrationsOnStartup = builder.Configuration.GetValue("Database:ApplyMigrationsOnStartup", false);
var migrateOnly = builder.Configuration.GetValue("Database:MigrateOnly", false);

if (tlsTerminatedAtProxy)
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });
}

builder.AddServiceDefaults();
builder.Services.AddAutoMapper(typeof(ACCmappingProfile));
builder.Services.Configure<ServiceEndpointsOptions>(builder.Configuration.GetSection(ServiceEndpointsOptions.SectionName));
var endpointOptions = builder.Configuration.GetSection(ServiceEndpointsOptions.SectionName).Get<ServiceEndpointsOptions>() ?? new ServiceEndpointsOptions();
var compilerBaseUrl = NormalizeBaseUrl(endpointOptions.CompilerBaseUrl, nameof(ServiceEndpointsOptions.CompilerBaseUrl));

var connectionString = ResolveConnectionString(
    builder.Configuration,
    "DefaultConnection",
    "acc-academic-db");

builder.Services.AddDbContext<ACCDbContext>(options =>
{
    options.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtIssuer = jwtSection["Issuer"];
var jwtAudience = jwtSection["Audience"];
var jwtKey = jwtSection["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("Missing 'Jwt:Key' configuration.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
            ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            NameClaimType = "sub",
            RoleClaimType = "role",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escribe: Bearer {tu-jwt}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var redisConnectionString = builder.Configuration.GetConnectionString("acc-redis");
if (!string.IsNullOrWhiteSpace(redisConnectionString))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnectionString;
    });
}
else
{
    builder.Services.AddDistributedMemoryCache();
}

builder.Services.AddAccExternalClients(builder.Configuration);
builder.Services.AddHttpClient<CompileService>(client =>
{
    client.BaseAddress = new Uri(compilerBaseUrl);
});
builder.Services.AddScoped<IBibliotecaService, BibliotecaService>();
builder.Services.AddScoped<IAgendaService, AgendaService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<ITareasPersonalesService, TareasPersonalesService>();
builder.Services.AddScoped<ITareasService, TareasAulaService>();
builder.Services.AddScoped<ITareasAlumnoService, TareasAlumnoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<UserSessionService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IAulaService, AulaService>();
builder.Services.AddScoped<IHistorialCalificacionesService, HistorialCalificacionesService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<ISubModuloService, SubModuloService>();
builder.Services.AddScoped<ISubTemaService, SubTemaService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IProgresoUsuarioService, ProgresoUsuarioService>();
builder.Services.AddScoped<ITipService, TipService>();
builder.Services.AddScoped<IAvisosService, AvisosService>();
builder.Services.AddScoped<INavegacionContenidoService, NavegacionContenidoService>();
builder.Services.AddScoped<IPrerrequisitosService, PrerrequisitosService>();
builder.Services.AddScoped<IExamenesModService, ExamenesService>();
builder.Services.AddScoped<IExamenesSubMService, ExamenesService>();
builder.Services.AddScoped<IExamenesUserService, ExamenesService>();
builder.Services.AddScoped<IExamenesHabilitadosService, ExamenesHabilitadosService>();
builder.Services.AddScoped<ILeccionesAdminService, LeccionesAdminService>();

builder.Services.AddSingleton<System.TimeProvider>(System.TimeProvider.System);
builder.Services.AddDataProtection();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (applyMigrationsOnStartup)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ACCDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (migrateOnly)
{
    return;
}

app.MapDefaultEndpoints();
if (tlsTerminatedAtProxy)
{
    app.UseForwardedHeaders();
}
else
{
    app.UseHttpsRedirection();
}
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

static string ResolveConnectionString(IConfiguration config, string connectionName, params string[] fallbackNames)
{
    var connectionString = config.GetConnectionString(connectionName);

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        foreach (var fallbackName in fallbackNames)
        {
            connectionString = config.GetConnectionString(fallbackName);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                break;
            }
        }
    }

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        var requestedNames = string.Join(", ", new[] { connectionName }.Concat(fallbackNames));
        throw new InvalidOperationException($"Connection string not found. Expected one of: {requestedNames}.");
    }

    if (connectionString.Contains("{SQL_PASSWORD}", StringComparison.Ordinal))
    {
        var sqlPassword = config["SqlPassword"];
        if (string.IsNullOrWhiteSpace(sqlPassword))
        {
            throw new InvalidOperationException("Missing 'SqlPassword' configuration for SQL connection string placeholder.");
        }

        connectionString = connectionString.Replace("{SQL_PASSWORD}", sqlPassword, StringComparison.Ordinal);
    }

    return connectionString;
}

static string NormalizeBaseUrl(string? url, string optionName)
{
    if (string.IsNullOrWhiteSpace(url))
    {
        throw new InvalidOperationException($"Service endpoint option '{optionName}' is missing.");
    }

    if (!Uri.TryCreate(url, UriKind.Absolute, out var parsedUri))
    {
        throw new InvalidOperationException($"Service endpoint option '{optionName}' is not a valid absolute URL.");
    }

    var normalized = parsedUri.ToString();
    return normalized.EndsWith("/", StringComparison.Ordinal) ? normalized : $"{normalized}/";
}
