using ACC.API.Extensions;
using ACC.API.Interfaces;
using ACC.API.Services;
using ACC.Data;
using ACC.ServiceDefaults;
using ACC.Shared.Core;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddAutoMapper(typeof(ACCmappingProfile));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

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
            ValidateIssuerSigningKey = !string.IsNullOrWhiteSpace(jwtKey),
            ValidateLifetime = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            NameClaimType = "sub",
            RoleClaimType = "role",
            IssuerSigningKey = string.IsNullOrWhiteSpace(jwtKey)
                ? null
                : new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
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

builder.Services.AddHttpClient();
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
builder.Services.AddScoped<CompileService>();
builder.Services.AddScoped<IProgresoUsuarioService, ProgresoUsuarioService>();
builder.Services.AddScoped<ITipService, TipService>();
builder.Services.AddScoped<IAvisosService, AvisosService>();
builder.Services.AddScoped<INavegacionContenidoService, NavegacionContenidoService>();
builder.Services.AddScoped<IPrerrequisitosService, PrerrequisitosService>();
builder.Services.AddScoped<IExamenesModService, ExamenesService>();
builder.Services.AddScoped<IExamenesSubMService, ExamenesService>();
builder.Services.AddScoped<IExamenesUserService, ExamenesService>();
builder.Services.AddScoped<IExamenesHabilitadosService, ExamenesHabilitadosService>();

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

app.MapDefaultEndpoints();
app.UseHttpsRedirection();
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
