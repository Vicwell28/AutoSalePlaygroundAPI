using AutoSalePlaygroundAPI.API.Middlewares;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auto Sale",
        Version = "v1"
    });

    c.EnableAnnotations();
});

// Registrar MediatR
//Básicamente, le dice a MediatR que busque en un ensamblado (en este caso, el que contiene la clase Program)
//todas las clases que implementen IRequestHandler<TRequest,TResponse>, INotificationHandler<TNotification>
//y demás tipos de handlers, para inyectarlas automáticamente.

// 1. Registrar MediatR
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllVehiclesQuery).Assembly));


// 2. Registrar FluentValidation (si lo usas con extension method)
//builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(GetAllVehiclesQuery).Assembly);


// 3. Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();


// Configurar el middleware de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Configurar middlewares
app.UseRequestResponseLoggingMiddleware();
app.UseExceptionHandlingMiddleware();

// Agregar puntos finales a la aplicación.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
