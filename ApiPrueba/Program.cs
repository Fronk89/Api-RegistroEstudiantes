using ApiPrueba.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var corsPolicy = "_myCorsPolicy";


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
// Configurar DbContext con SQL Server
builder.Services.AddDbContext<BdRegistroEscolarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7174, listenOptions =>
    {
        listenOptions.UseHttps(); // O remover si no hay SSL configurado
    });
});


// Agregar controladores (API)
builder.Services.AddControllers();

// Agregar servicios de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(corsPolicy);  // Usar la política CORS

app.MapControllers();

// Iniciar la aplicación
app.Run();
