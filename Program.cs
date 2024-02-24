using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Web API desafio 21 dias Pais",
        Version = "v1",
        Description = "API feita para pais dos alunos no desafio 21 dias"
    });
});

string? AlunoApi = builder.Configuration.GetConnectionString("AlunoApi");

string? strCon = builder.Configuration.GetConnectionString("MinhaConexao");
builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio 21 dias"));

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();