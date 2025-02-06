using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Infrastructure.Seeders
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AutoSalePlaygroundAPIDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Accessories.AnyAsync())
            {
                var accessories = new List<Accessory>
                {
                    new Accessory("Tapicería de cuero"),
                    new Accessory("Sistema de sonido premium"),
                    new Accessory("Sensores de estacionamiento"),
                    new Accessory("Cámara de visión trasera"),
                    new Accessory("Frenos ABS"),
                    new Accessory("Aire acondicionado automático"),
                    new Accessory("Asistente de cambio de carril"),
                    new Accessory("Techo solar"),
                    new Accessory("Llantas de aleación"),
                    new Accessory("Control de crucero adaptativo")
                };

                context.Accessories.AddRange(accessories);
                await context.SaveChangesAsync();
            }

            if (!await context.Owners.AnyAsync())
            {
                var owners = new List<Owner>
                {
                    new Owner("Juan", "Pérez"),
                    new Owner("María", "Gómez"),
                    new Owner("Luis", "Rodríguez"),
                    new Owner("Ana", "Martínez"),
                    new Owner("Carlos", "Sánchez"),
                    new Owner("Laura", "Ramírez"),
                    new Owner("Miguel", "Torres"),
                    new Owner("Sofía", "Vargas"),
                    new Owner("Diego", "Castro"),
                    new Owner("Elena", "Díaz")
                };

                context.Owners.AddRange(owners);
                await context.SaveChangesAsync();
            }


            if (!await context.Vehicles.AnyAsync())
            {
                var ownersList = await context.Owners.ToListAsync();

                var vehicles = new List<Vehicle>
                {
                    new Vehicle("ABC-123", ownersList[0], new Specifications("Gasolina", 1600, 120)),
                    new Vehicle("DEF-456", ownersList[1], new Specifications("Diesel", 2000, 150)),
                    new Vehicle("GHI-789", ownersList[2], new Specifications("Gasolina", 1800, 130)),
                    new Vehicle("JKL-012", ownersList[3], new Specifications("Eléctrico", 1500, 200)),
                    new Vehicle("MNO-345", ownersList[4], new Specifications("Gasolina", 1400, 110)),
                    new Vehicle("PQR-678", ownersList[5], new Specifications("Diesel", 2200, 160)),
                    new Vehicle("STU-901", ownersList[6], new Specifications("Gasolina", 2000, 140)),
                    new Vehicle("VWX-234", ownersList[7], new Specifications("Híbrido", 1600, 125)),
                    new Vehicle("YZA-567", ownersList[8], new Specifications("Gasolina", 1500, 115)),
                    new Vehicle("BCD-890", ownersList[9], new Specifications("Diesel", 2100, 155))
                };

                context.Vehicles.AddRange(vehicles);
                await context.SaveChangesAsync();
            }
        }
    }
}
