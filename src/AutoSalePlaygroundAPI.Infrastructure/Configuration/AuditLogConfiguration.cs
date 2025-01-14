using AutoSalePlaygroundAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Infrastructure.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            entity.ToTable("AuditLogs");
            entity.HasKey(a => a.Id);

            entity.Property(a => a.EntityName).HasMaxLength(200);
            entity.Property(a => a.EventType).HasMaxLength(200);
            entity.Property(a => a.OldValues).HasColumnType("nvarchar(max)");
            entity.Property(a => a.NewValues).HasColumnType("nvarchar(max)");
        }
    }
}
