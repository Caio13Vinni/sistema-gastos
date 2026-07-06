using System.Text.Json.Serialization;
using SistemaGastos.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Mostra o tipo da transação como  JSON.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();

// SQLite salvo em arquivo local
var connectionString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=controlegastos.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Permissao frontend
const string CorsPolicyName = "PermitirFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
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
app.UseAuthorization();
app.MapControllers();

app.Run();