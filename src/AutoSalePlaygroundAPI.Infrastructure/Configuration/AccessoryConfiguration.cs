using AutoSalePlaygroundAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSalePlaygroundAPI.Infrastructure.Configuration
{
    public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
    {
        public void Configure(EntityTypeBuilder<Accessory> entity)
        {
            entity.ToTable("Accessories");
            entity.HasKey(a => a.Id);

            entity.Property(a => a.CreatedAt).IsRequired();
            entity.Property(a => a.IsActive).HasDefaultValue(true);
        }
    }
}
