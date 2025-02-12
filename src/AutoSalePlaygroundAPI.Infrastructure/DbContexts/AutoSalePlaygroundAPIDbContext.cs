using AutoSalePlaygroundAPI.Domain.DomainEvent;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AutoSalePlaygroundAPI.Infrastructure.DbContexts
{
    /// <summary>
    /// Representa el contexto de base de datos de la aplicación AutoSalePlaygroundAPI.
    /// 
    /// Este DbContext maneja las entidades principales: <see cref="Vehicle"/>, <see cref="Owner"/>,
    /// <see cref="Accessory"/> y <see cref="AuditLog"/>. Además, incorpora la lógica para procesar
    /// los <see cref="IDomainEvent"/> generados durante la operación del dominio.
    /// 
    /// <para>
    /// <strong>Funcionamiento del Domain Event:</strong>
    /// Las entidades del dominio pueden generar eventos (Domain Events) para notificar que ha ocurrido
    /// un cambio significativo (por ejemplo, actualización de propietario, modificación de matrícula, etc.).
    /// Estos eventos se acumulan en una lista interna (<see cref="DomainEvents.Events"/>). Al invocar los métodos
    /// <c>SaveChanges</c> o <c>SaveChangesAsync</c>, el contexto:
    /// <list type="number">
    ///   <item>Guarda los cambios iniciales en la base de datos.</item>
    ///   <item>Procesa los eventos de dominio pendientes, mapeándolos a registros de auditoría (<see cref="AuditLog"/>)
    ///         mediante el método <see cref="ProcessDomainEvents"/> y <see cref="MapDomainEventToAuditLog"/>.</item>
    ///   <item>Guarda nuevamente para persistir los registros de auditoría generados.</item>
    ///   <item>Finalmente, limpia la lista de eventos pendientes para evitar reprocesos.</item>
    /// </list>
    /// </para>
    /// </summary>
    public class AutoSalePlaygroundAPIDbContext : DbContext
    {
        /// <summary>
        /// Conjunto de entidades de tipo <see cref="Vehicle"/>.
        /// </summary>
        public DbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Conjunto de entidades de tipo <see cref="Owner"/>.
        /// </summary>
        public DbSet<Owner> Owners { get; set; }

        /// <summary>
        /// Conjunto de entidades de tipo <see cref="Accessory"/>.
        /// </summary>
        public DbSet<Accessory> Accessories { get; set; }

        /// <summary>
        /// Conjunto de entidades de tipo <see cref="AuditLog"/>.
        /// Aquí se almacenan los registros de auditoría generados a partir de eventos de dominio.
        /// </summary>
        public DbSet<AuditLog> AuditLogs { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia del contexto de base de datos con las opciones especificadas.
        /// </summary>
        /// <param name="options">Opciones de configuración para el DbContext.</param>
        public AutoSalePlaygroundAPIDbContext(DbContextOptions<AutoSalePlaygroundAPIDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configura los modelos y sus relaciones en el modelo de datos.
        /// Aplica configuraciones específicas para cada entidad utilizando clases de configuración.
        /// </summary>
        /// <param name="modelBuilder">El constructor de modelos utilizado para configurar las entidades.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new AccessoryConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Guarda los cambios en la base de datos de forma síncrona.
        /// 
        /// Realiza los siguientes pasos:
        /// <list type="number">
        ///   <item>Guarda los cambios iniciales en la base de datos.</item>
        ///   <item>Procesa los eventos de dominio pendientes y genera los registros de auditoría correspondientes.</item>
        ///   <item>Guarda nuevamente para persistir los registros de auditoría.</item>
        /// </list>
        /// </summary>
        /// <returns>El número de cambios escritos a la base de datos.</returns>
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

        /// <summary>
        /// Guarda los cambios en la base de datos de forma asíncrona.
        /// 
        /// Realiza los siguientes pasos:
        /// <list type="number">
        ///   <item>Guarda los cambios iniciales en la base de datos.</item>
        ///   <item>Procesa los eventos de dominio pendientes y genera los registros de auditoría correspondientes.</item>
        ///   <item>Guarda nuevamente para persistir los registros de auditoría.</item>
        /// </list>
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación para la operación asíncrona.</param>
        /// <returns>Una tarea que representa la operación asíncrona. El resultado es el número de cambios escritos a la base de datos.</returns>
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
        /// Procesa los eventos de dominio pendientes.
        /// 
        /// Extrae los eventos de dominio acumulados en <see cref="DomainEvents.Events"/>,
        /// los mapea a objetos <see cref="AuditLog"/> mediante el método <see cref="MapDomainEventToAuditLog"/>,
        /// y los agrega al DbSet correspondiente. Finalmente, limpia la lista de eventos.
        /// </summary>
        private void ProcessDomainEvents()
        {
            // Se obtiene la lista de eventos de dominio y se mapea cada uno a un objeto AuditLog.
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
        /// Mapea un evento de dominio a un objeto <see cref="AuditLog"/> utilizando una expresión <c>switch</c>.
        /// Retorna <c>null</c> si el evento no es reconocido.
        /// </summary>
        /// <param name="domainEvent">El evento de dominio a mapear.</param>
        /// <returns>
        /// Un objeto <see cref="AuditLog"/> que contiene los detalles del evento (nombre de la entidad, identificador,
        /// tipo de evento, valores anteriores y nuevos, fecha de ocurrencia) o <c>null</c> si el evento no se reconoce.
        /// </returns>
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
