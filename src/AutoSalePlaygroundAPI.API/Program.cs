using AutoSalePlaygroundAPI.API.Middlewares;
using AutoSalePlaygroundAPI.Application.Behaviors;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle;
using AutoSalePlaygroundAPI.Application.Mappings;
using AutoSalePlaygroundAPI.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AutoSalePlaygroundAPIDbContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Scoped);

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
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllVehiclesQuery).Assembly));

// Registrar FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(GetAllVehiclesQuery).Assembly);

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(VehicleProfile).Assembly);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registrar Pipeline Behavior para validaci�n
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),  typeof(TransactionBehavior<,>));

var app = builder.Build();

// **Inicio de la Prueba de Conexi�n a la Base de Datos**
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();

//    try
//    {
//        var dbContext = services.GetRequiredService<AutoSalePlaygroundAPIDbContext>();

//        if (dbContext.Database.CanConnect())
//        {
//            logger.LogInformation("Conexi�n exitosa a la base de datos.");
//        }
//        else
//        {
//            logger.LogError("No se pudo establecer conexi�n con la base de datos.");
//        }
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "Ocurri� un error al intentar conectar a la base de datos.");
//    }
//}
// **Fin de la Prueba de Conexi�n a la Base de Datos**


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

// Configurar middlewares personalizados
app.UseRequestResponseLoggingMiddleware();
app.UseExceptionHandlingMiddleware();

// Agregar puntos finales a la aplicaci�n.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
