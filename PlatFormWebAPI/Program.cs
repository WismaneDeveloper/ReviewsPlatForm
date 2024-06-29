using Entities.ContextDB;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// agregamos este servicio para evitar que se genere un bucle en las respuestas json
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// configuracion del acceso a datos.
var connection = builder.Configuration.GetConnectionString("ChainSql"); // creamos una variable que contenga la cadena de conexion 
// contruimos el contexto de conexion
builder.Services.AddDbContext<LibrosdbContext>(opt => opt.UseSqlServer(connection));


// permitimos que UsuarioServicio o cualquier otra entidad sea instanciada...
//...configurar las interfacez para que el controlador las pueda usar 
builder.Services.AddServicesDependencies();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7293")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
