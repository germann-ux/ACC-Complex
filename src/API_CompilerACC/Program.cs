using API_CompilerACC.Interfaces;
using API_CompilerACC.Services;
using ACC.ServiceDefaults; 

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults(); // telemetría, healtcheks, etc.(usado para que aspire detecte)
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// servicio de compilacion / simple
//builder.Services.AddSingleton<ICompileService, CompileService>();
builder.Services.AddSingleton<ICompileService, RoslynCompileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
