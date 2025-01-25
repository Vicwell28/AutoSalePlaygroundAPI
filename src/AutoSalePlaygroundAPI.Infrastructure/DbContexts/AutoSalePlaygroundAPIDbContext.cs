using AutoSalePlaygroundAPI.Domain.DomainEvent;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Infrastructure.DbContexts
{
    public class AutoSalePlaygroundAPIDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public AutoSalePlaygroundAPIDbContext() { }
        public AutoSalePlaygroundAPIDbContext(DbContextOptions<AutoSalePlaygroundAPIDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        // Con el unico proposito de hacer las migraciones
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
            base.OnConfiguring(optionsBuilder);
        }



        public override int SaveChanges()
        {
            var domainEvents = new List<IDomainEvent>(DomainEvents.Events);

            var result = base.SaveChanges();

            foreach (var domainEvent in domainEvents)
            {
                var auditLog = MapDomainEventToAuditLog(domainEvent);

                if (auditLog != null)
                {
                    AuditLogs.Add(auditLog);
                }
            }

            DomainEvents.Clear();

            base.SaveChanges();

            return result;
        }

        private AuditLog? MapDomainEventToAuditLog(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case VehicleOwnerChangedDomainEvent voc:
                    return new AuditLog
                    {
                        EntityName = nameof(Vehicle),
                        EntityId = voc.VehicleId,
                        EventType = nameof(VehicleOwnerChangedDomainEvent),
                        OldValues = $"OldOwnerId={voc.OldOwnerId}",
                        NewValues = $"NewOwnerId={voc.NewOwnerId}",
                        OccurredOn = voc.OccurredOn
                    };

                case VehicleLicensePlateUpdatedDomainEvent vlu:
                    return new AuditLog
                    {
                        EntityName = nameof(Vehicle),
                        EntityId = vlu.VehicleId,
                        EventType = nameof(VehicleLicensePlateUpdatedDomainEvent),
                        OldValues = $"OldPlate={vlu.OldPlate}",
                        NewValues = $"NewPlate={vlu.NewPlate}",
                        OccurredOn = vlu.OccurredOn
                    };

                case VehicleAccessoryAddedDomainEvent vaa:
                    return new AuditLog
                    {
                        EntityName = nameof(Vehicle),
                        EntityId = vaa.VehicleId,
                        EventType = nameof(VehicleAccessoryAddedDomainEvent),
                        OldValues = string.Empty,
                        NewValues = $"AccessoryId={vaa.AccessoryId}",
                        OccurredOn = vaa.OccurredOn
                    };

                case VehicleSpecificationsUpdatedDomainEvent vsu:
                    return new AuditLog
                    {
                        EntityName = nameof(Vehicle),
                        EntityId = vsu.VehicleId,
                        EventType = nameof(VehicleSpecificationsUpdatedDomainEvent),
                        OldValues = $"OldFuelType={vsu.OldFuelType}," +
                                    $"OldEngineDisplacement={vsu.OldEngineDisplacement}," +
                                    $"OldHorsepower={vsu.OldHorsepower}",
                        NewValues = $"NewFuelType={vsu.NewFuelType}," +
                                    $"NewEngineDisplacement={vsu.NewEngineDisplacement}," +
                                    $"NewHorsepower={vsu.NewHorsepower}",
                        OccurredOn = vsu.OccurredOn
                    };

                case OwnerUpdatedDomainEvent ou:
                    return new AuditLog
                    {
                        EntityName = nameof(Owner),
                        EntityId = ou.OwnerId,
                        EventType = nameof(OwnerUpdatedDomainEvent),
                        OldValues = string.Empty,
                        NewValues = $"NewFirstName={ou.NewFirstName},NewLastName={ou.NewLastName}",
                        OccurredOn = ou.OccurredOn
                    };

                case AccessoryUpdatedDomainEvent au:
                    return new AuditLog
                    {
                        EntityName = nameof(Accessory),
                        EntityId = au.AccessoryId,
                        EventType = nameof(AccessoryUpdatedDomainEvent),
                        OldValues = string.Empty,
                        NewValues = $"NewName={au.NewName}",
                        OccurredOn = au.OccurredOn
                    };
                default:
                    return null;
            }
        }
    }
}
