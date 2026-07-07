using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SistemaGastos.API.Data;
using SistemaGastos.API.Services;
using SistemaGastos.API.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Mostra o tipo da transação como JSON e IGNORA CICLOS INFINITOS
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    { 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

// SQLite salvo em arquivo local
var connectionString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=controlegastos.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Registrando os Services ANTES do builder.Build()
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<TransacaoService>();
builder.Services.AddScoped<RelatorioService>();

// Permissao frontend
const string CorsPolicyName = "PermitirFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {

        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var app = builder.Build();

// Cria o banco e as tabelas automaticamente na primeira execução.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
app.UseHttpsRedirection();
app.UseCors(CorsPolicyName);
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();