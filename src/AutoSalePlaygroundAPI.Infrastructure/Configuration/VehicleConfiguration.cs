using AutoSalePlaygroundAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSalePlaygroundAPI.Infrastructure.Configuration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> entity)
        {
            entity.ToTable("Vehicles");
            entity.HasKey(v => v.Id);

            entity.Property(v => v.CreatedAt).IsRequired();
            entity.Property(v => v.IsActive).HasDefaultValue(true);

            // Relationship with Owner
            entity.HasOne(v => v.Owner)
                  .WithMany(o => o.Vehicles)
                  .HasForeignKey(v => v.OwnerId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Owned: Specifications
            entity.OwnsOne(v => v.Specifications, specs =>
            {
                specs.Property(s => s.FuelType).HasColumnName("FuelType");
                specs.Property(s => s.EngineDisplacement).HasColumnName("EngineDisplacement");
                specs.Property(s => s.Horsepower).HasColumnName("Horsepower");
            });

            // Many-to-many with Accessories
            entity.HasMany(v => v.Accessories)
                  .WithMany(a => a.Vehicles)
                  .UsingEntity<Dictionary<string, object>>(
                      "VehicleAccessory",
                      join => join
                        .HasOne<Accessory>()
                        .WithMany()
                        .HasForeignKey("AccessoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                      join => join
                        .HasOne<Vehicle>()
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                  );
        }
    }

}
