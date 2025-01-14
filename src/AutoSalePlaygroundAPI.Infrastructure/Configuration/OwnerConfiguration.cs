using AutoSalePlaygroundAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSalePlaygroundAPI.Infrastructure.Configuration
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> entity)
        {
            entity.ToTable("Owners");
            entity.HasKey(o => o.Id);

            entity.Property(o => o.CreatedAt).IsRequired();
            entity.Property(o => o.IsActive).HasDefaultValue(true);
        }
    }
}
