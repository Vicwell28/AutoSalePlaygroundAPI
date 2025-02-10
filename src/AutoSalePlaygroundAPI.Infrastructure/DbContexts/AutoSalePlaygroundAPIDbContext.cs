using AutoSalePlaygroundAPI.Domain.DomainEvent;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Infrastructure.DbContexts
{
    public class AutoSalePlaygroundAPIDbContext(DbContextOptions<AutoSalePlaygroundAPIDbContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Guarda los cambios iniciales
            var result = base.SaveChanges();

            // Procesa y agrega los registros de auditoría correspondientes a los eventos de dominio
            ProcessDomainEvents();

            // Persiste los logs de auditoría en la base de datos
            base.SaveChanges();

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Guarda los cambios iniciales
            var result = await base.SaveChangesAsync(cancellationToken);

            // Procesa y agrega los registros de auditoría correspondientes a los eventos de dominio
            ProcessDomainEvents();

            // Persiste los logs de auditoría en la base de datos
            await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// Extrae los eventos de dominio pendientes, los mapea a registros de auditoría y los agrega al DbSet correspondiente.
        /// Además, limpia la lista de eventos.
        /// </summary>
        private void ProcessDomainEvents()
        {
            // Se obtiene la lista de eventos de dominio
            var auditLogs = DomainEvents.Events
                .Select(MapDomainEventToAuditLog)
                .Where(auditLog => auditLog is not null)
                .Cast<AuditLog>()
                .ToList();

            // Si se encontraron eventos, se agregan los logs resultantes
            if (auditLogs.Any())
            {
                AuditLogs.AddRange(auditLogs);
            }

            // Se limpia la lista de eventos pendientes
            DomainEvents.Clear();
        }

        /// <summary>
        /// Mapea un evento de dominio a un objeto AuditLog usando una expresión switch.
        /// Retorna null si el evento no es reconocido.
        /// </summary>
        private AuditLog? MapDomainEventToAuditLog(IDomainEvent domainEvent) =>
            domainEvent switch
            {
                VehicleOwnerChangedDomainEvent voc => new AuditLog
                {
                    EntityName = nameof(Vehicle),
                    EntityId = voc.VehicleId,
                    EventType = nameof(VehicleOwnerChangedDomainEvent),
                    OldValues = $"OldOwnerId={voc.OldOwnerId}",
                    NewValues = $"NewOwnerId={voc.NewOwnerId}",
                    OccurredOn = voc.OccurredOn
                },
                VehicleLicensePlateUpdatedDomainEvent vlu => new AuditLog
                {
                    EntityName = nameof(Vehicle),
                    EntityId = vlu.VehicleId,
                    EventType = nameof(VehicleLicensePlateUpdatedDomainEvent),
                    OldValues = $"OldPlate={vlu.OldPlate}",
                    NewValues = $"NewPlate={vlu.NewPlate}",
                    OccurredOn = vlu.OccurredOn
                },
                VehicleAccessoryAddedDomainEvent vaa => new AuditLog
                {
                    EntityName = nameof(Vehicle),
                    EntityId = vaa.VehicleId,
                    EventType = nameof(VehicleAccessoryAddedDomainEvent),
                    OldValues = string.Empty,
                    NewValues = $"AccessoryId={vaa.AccessoryId}",
                    OccurredOn = vaa.OccurredOn
                },
                VehicleSpecificationsUpdatedDomainEvent vsu => new AuditLog
                {
                    EntityName = nameof(Vehicle),
                    EntityId = vsu.VehicleId,
                    EventType = nameof(VehicleSpecificationsUpdatedDomainEvent),
                    OldValues = $"OldFuelType={vsu.OldFuelType},OldEngineDisplacement={vsu.OldEngineDisplacement},OldHorsepower={vsu.OldHorsepower}",
                    NewValues = $"NewFuelType={vsu.NewFuelType},NewEngineDisplacement={vsu.NewEngineDisplacement},NewHorsepower={vsu.NewHorsepower}",
                    OccurredOn = vsu.OccurredOn
                },
                OwnerUpdatedDomainEvent ou => new AuditLog
                {
                    EntityName = nameof(Owner),
                    EntityId = ou.OwnerId,
                    EventType = nameof(OwnerUpdatedDomainEvent),
                    OldValues = string.Empty,
                    NewValues = $"NewFirstName={ou.NewFirstName},NewLastName={ou.NewLastName}",
                    OccurredOn = ou.OccurredOn
                },
                AccessoryUpdatedDomainEvent au => new AuditLog
                {
                    EntityName = nameof(Accessory),
                    EntityId = au.AccessoryId,
                    EventType = nameof(AccessoryUpdatedDomainEvent),
                    OldValues = string.Empty,
                    NewValues = $"NewName={au.NewName}",
                    OccurredOn = au.OccurredOn
                },
                _ => null
            };
    }
}
