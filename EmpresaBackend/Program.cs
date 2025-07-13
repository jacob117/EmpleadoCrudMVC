using Microsoft.OpenApi.Models;
using EmpresaBackend.Data;
using EmpresaBackend.Services;

var builder = WebApplication.CreateBuilder(args);
// Agrega servicios al contenedor
builder.Services.AddControllers();

// Configura Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Empresa",
        Version = "v1",
        Description = "API para gestión de empleados y jerarquía"
    });
});

// Registra tus servicios personalizados
builder.Services.AddScoped<DbConnector>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<CatalogoService>();

var app = builder.Build();

// Configura el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Empresa v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:5000/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
