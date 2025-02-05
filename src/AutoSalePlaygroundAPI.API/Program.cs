using AutoSalePlaygroundAPI.API.Middlewares;
using AutoSalePlaygroundAPI.Application.Behaviors;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Application.Mappings;
using AutoSalePlaygroundAPI.Application.Services;
using AutoSalePlaygroundAPI.Infrastructure;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AutoSalePlaygroundAPIDbContext>(options =>
{
    options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
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

// Registro del repositorio genérico y Unit of Work:
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registro de los servicios de aplicación:
builder.Services.AddScoped<IAccessoryService, AccessoryService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

// Registrar Pipeline Behavior para validación
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

var app = builder.Build();

// **Inicio de la Prueba de Conexión a la Base de Datos**
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var logger = services.GetRequiredService<ILogger<Program>>();

//    try
//    {
//        var dbContext = services.GetRequiredService<AutoSalePlaygroundAPIDbContext>();

//        if (dbContext.Database.CanConnect())
//        {
//            logger.LogInformation("Conexión exitosa a la base de datos.");
//        }
//        else
//        {
//            logger.LogError("No se pudo establecer conexión con la base de datos.");
//        }
//    }
//    catch (Exception ex)
//    {
//        logger.LogError(ex, "Ocurrió un error al intentar conectar a la base de datos.");
//    }
//}
// **Fin de la Prueba de Conexión a la Base de Datos**


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

// Agregar puntos finales a la aplicación.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
