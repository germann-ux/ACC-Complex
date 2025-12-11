using ACC.Data;
using ACC.API.Services;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using ACC.API.Extensions;
using ACC.Shared.Core;
using ACC.API.Interfaces;
using ACC.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Text.Json;

// ============================ =============== //
// ---- ACC.API - Program.cs 
// ============================ =============== //

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // telemetry, quizas quitar si no funciona

// AutoMapper
builder.Services.AddAutoMapper(typeof(ACCmappingProfile));

//Cadenas de Conexión a base de datos académica//

// desarrollo: "DefaultConnection"
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// producción: "acc-academic-db"
//var connectionString = builder.Configuration.GetConnectionString("acc-academic-db") ?? throw new InvalidOperationException("Connection string 'acc-academic-db' not found.");

Console.WriteLine($"CADENA DE CONEXION CORRECTA: {connectionString}"); // important

//builder.Services.AddDbContext<ACCDbContext>(options =>
//    options.UseSqlServer(connectionString));

// Configuración del DbContext con reintentos en caso de fallos de conexión
builder.Services.AddDbContext<ACCDbContext>(options =>
{
    options.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios personalizados
builder.Services.AddHttpClient();
//builder.Services.AddScoped<IBloquesRenderService, BloquesRenderService>(); servicio cancelado por falta de tiempo para su implementación
builder.Services.AddScoped<IBibliotecaService, BibliotecaService>();
builder.Services.AddScoped<IAgendaService, AgendaService>();
builder.Services.AddScoped<IModuloService, ModuloService>();
builder.Services.AddScoped<ITareaService, TareasService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>(); // Este puede renombrarse luego
builder.Services.AddSingleton<UserSessionService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IAulaService, AulaService>();
builder.Services.AddScoped<IHistorialCalificacionesService, HistorialCalificacionesService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<ISubModuloService, SubModuloService>();
builder.Services.AddScoped<ISubTemaService, SubTemaService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<CompileService>();
builder.Services.AddScoped<IProgresoUsuarioService, ProgresoUsuarioService>();
builder.Services.AddScoped<ITipService, TipService>();
builder.Services.AddScoped<IAvisosService, AvisosService>();
builder.Services.AddScoped<INavegacionContenidoService, NavegacionContenidoService>();
builder.Services.AddScoped<ICapitulosService, CapitulosService>();
// --- Exámenes:
builder.Services.AddScoped<IPrerrequisitosService, PrerrequisitosService>();
builder.Services.AddScoped<IExamenesModService, ExamenesService>();
builder.Services.AddScoped<IExamenesSubMService, ExamenesService>();
builder.Services.AddScoped<IExamenesUserService, ExamenesService>();
// Fachada de compatibilidad:
builder.Services.AddScoped<IExamenesHabilitadosService, ExamenesHabilitadosService>();

// Tiempo del sistema
builder.Services.AddSingleton<System.TimeProvider>(System.TimeProvider.System);

// Protección de datos (por si lo usas para tokens más adelante)
builder.Services.AddDataProtection();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy./*WithOrigins("https://localhost:7160", "http://localhost:5295", "https://localhost:7023")*/AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
//{
//    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
//});

// Para controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Para endpoints, minimal APIs, etc.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


//(Opcional) Configurar autenticación JWT más adelante
var app = builder.Build();

app.MapDefaultEndpoints(); // Mapeo de endpoints de salud
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Estos se activarán solo si usamos JWT
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();
app.Run();
