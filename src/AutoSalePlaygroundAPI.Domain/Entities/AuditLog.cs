namespace AutoSalePlaygroundAPI.Domain.Entities
{
    /// <summary>
    /// Representa un registro de auditoría que captura los cambios realizados en las entidades.
    /// Se utiliza para almacenar la información de los eventos de dominio que afectan a las entidades.
    /// </summary>
    public class AuditLog : IEntity
    {
        /// <summary>
        /// Obtiene o establece el identificador del registro de auditoría.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la entidad afectada.
        /// </summary>
        public string EntityName { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece el identificador de la entidad afectada.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Obtiene o establece el tipo de evento que ocurrió.
        /// </summary>
        public string EventType { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece los valores anteriores antes del cambio.
        /// </summary>
        public string OldValues { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece los nuevos valores luego del cambio.
        /// </summary>
        public string NewValues { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la fecha y hora en que ocurrió el evento.
        /// </summary>
        public DateTime OccurredOn { get; set; }
    }
}
