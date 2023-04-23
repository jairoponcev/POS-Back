using POS.Application.Extensions;
using POS.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
var Cors = "Cors"; // Declarar variable con el nombre de nuestra politicas CORS

builder.Services.AddInjectionInfrastructure(Configuration);
builder.Services.AddInjectionApplication(Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracion de las politicas cors para la variable con el nombre de nuestras politicas:
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

// Inicializamos nuestras politicas:
app.UseCors(Cors);

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

public partial class Program { }