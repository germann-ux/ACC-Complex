using ACC.WebApp.Client.Components.Pages;
using ACC.WebApp.Components;
using ACC.WebApp.Components.Account;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using ACC.WebApp.Services;
using ACC.WebApp.Data;
using ACC.ServiceDefaults; 
using ACC.WebApp.Client.Services;
using ACC.Shared.Utils;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;

// ============================ =============== //
// ---- ACC.WebApp - Program.cs 
// ============================ =============== //

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

builder.Services.Configure<ServiceEndpointsOptions>(builder.Configuration.GetSection(ServiceEndpointsOptions.SectionName));
var endpointOptions = builder.Configuration.GetSection(ServiceEndpointsOptions.SectionName).Get<ServiceEndpointsOptions>() ?? new ServiceEndpointsOptions();
var apiBaseUrl = NormalizeBaseUrl(endpointOptions.ApiBaseUrl, nameof(ServiceEndpointsOptions.ApiBaseUrl));
var compilerBaseUrl = NormalizeBaseUrl(endpointOptions.CompilerBaseUrl, nameof(ServiceEndpointsOptions.CompilerBaseUrl));

builder.AddServiceDefaults();// quizas quitar luego...

builder.Services.AddAutoMapper(typeof(UserMappingProfile));
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<BibliotecaClientService>();
builder.Services.AddScoped<ProgresoUsuarioClient>();
//builder.Services.AddScoped<BloquesContenidoClient>(); // servicio cancelado por falta de tiempo para su implementación
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IRoleStateService, RoleStateService>();
builder.Services.AddScoped<UsuarioSyncService>();
builder.Services.AddScoped<IApiJwtTokenService, ApiJwtTokenService>();
builder.Services.AddScoped<ApiJwtAuthDelegatingHandler>();
builder.Services.AddScoped<CharpClientService>();
builder.Services.AddScoped<LeccionesAdminClientService>();
builder.Services.AddScoped<NavegacionContenidoClient>();
builder.Services.AddScoped<TareasAulaClient>();
builder.Services.AddScoped<AgendaClientService>();
builder.Services.AddScoped<AgendaRealtimeNotifier>();
// --- Examenes:
builder.Services.AddScoped<ExamenesServiceClient>();
// Program.cs (WebApp)
// Cadenas de Conexión a base de datos de identidad // 

// desarrollo: "DefaultConnection"
var connectionString = ResolveConnectionString(
    builder.Configuration,
    "DefaultConnection",
    "acc-identity-db");

var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("Missing 'Jwt:Key' configuration.");
}

// producción: "acc-identity-db"
//var connectionString = builder.Configuration.GetConnectionString("acc-identity-db") ?? throw new InvalidOperationException("Connection string 'acc-identity-db' not found.");

Console.WriteLine($"🔌Cadena de conexión (sql-Identity): {connectionString}");

// descartado.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString),
//    sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
//    );

// Configuración del DbContext con reintentos en caso de fallos de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


builder.Services.AddHttpClient("ACC.API", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddScoped(sp =>
{
    var authHandler = sp.GetRequiredService<ApiJwtAuthDelegatingHandler>();
    authHandler.InnerHandler = new HttpClientHandler();

    return new HttpClient(authHandler)
    {
        BaseAddress = new Uri(apiBaseUrl)
    };
});
builder.Services.AddHttpClient<CompilerClientService>(client =>
{
    client.BaseAddress = new Uri(compilerBaseUrl);
});

//Habilita la compresión
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
options.Providers.Add<GzipCompressionProvider>();
options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7160", "http://localhost:5295", "https://localhost:7023")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (applyMigrationsOnStartup)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (migrateOnly)
{
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors(); 
if (tlsTerminatedAtProxy)
{
    app.UseForwardedHeaders();
}
else
{
    app.UseHttpsRedirection();
}
app.UseResponseCompression();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapDefaultEndpoints();// btw quizas quitar luego..

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ACC.WebApp.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Crear roles al iniciar si no existen
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = ["Administrador", "Docente", "Estudiante"];

    foreach (var role in roles)
    {
        var exists = await roleManager.RoleExistsAsync(role);
        if (!exists)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

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
