using ACC.WebApp.Client.Components.Pages;
using ACC.WebApp.Components;
using ACC.WebApp.Components.Account;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using ACC.WebApp.Services;
using ACC.WebApp.Data;
using ACC.ServiceDefaults; 
using ACC.WebApp.Client.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Blazored.LocalStorage;

// ============================ =============== //
// ---- ACC.WebApp - Program.cs 
// ============================ =============== //

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<NavegacionContenidoClient>();
builder.Services.AddScoped<TareasAulaClient>();
builder.Services.AddScoped<AgendaClientService>();
builder.Services.AddScoped<AgendaRealtimeNotifier>();
// --- Examenes:
builder.Services.AddScoped<ExamenesServiceClient>();
// Program.cs (WebApp)
// Cadenas de Conexión a base de datos de identidad // 

// desarrollo: "DefaultConnection"
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

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
    client.BaseAddress = new Uri(uriString: builder.Configuration["ApiUrl"]);
});

builder.Services.AddScoped(sp =>
{
    var apiUrl = sp.GetRequiredService<IConfiguration>()["ApiUrl"]
        ?? throw new InvalidOperationException("ApiUrl no configurada.");

    var authHandler = sp.GetRequiredService<ApiJwtAuthDelegatingHandler>();
    authHandler.InnerHandler = new HttpClientHandler();

    return new HttpClient(authHandler)
    {
        BaseAddress = new Uri(apiUrl)
    };
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
app.UseHttpsRedirection();
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
