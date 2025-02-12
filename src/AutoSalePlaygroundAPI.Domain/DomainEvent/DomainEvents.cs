namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    /// <summary>
    /// Proporciona un mecanismo para acumular y gestionar los eventos de dominio que ocurren en la aplicación.
    /// 
    /// <para>
    /// Los <strong>Domain Events</strong> permiten notificar de manera desacoplada a otras partes de la aplicación
    /// que ha ocurrido un cambio significativo en el dominio (por ejemplo, cambios en entidades o acciones relevantes).
    /// Las entidades pueden "disparar" estos eventos utilizando el método <see cref="Raise(IDomainEvent)"/>.
    /// Posteriormente, el contexto (o algún otro componente) puede procesarlos, realizar tareas secundarias (como la auditoría,
    /// notificaciones, etc.) y limpiar la lista de eventos mediante el método <see cref="Clear()"/>.
    /// </para>
    /// </summary>
    public static class DomainEvents
    {
        private static List<IDomainEvent> _events = new List<IDomainEvent>();

        /// <summary>
        /// Obtiene una colección de solo lectura con los eventos de dominio actualmente acumulados.
        /// </summary>
        public static IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        /// <summary>
        /// Agrega un evento de dominio a la colección.
        /// </summary>
        /// <param name="domainEvent">El evento de dominio que se desea agregar.</param>
        public static void Raise(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        /// <summary>
        /// Limpia la colección de eventos de dominio.
        /// </summary>
        public static void Clear() => _events.Clear();
    }

    /// <summary>
    /// Define el contrato para los eventos de dominio.
    /// Cada evento implementa esta interfaz para indicar cuándo ocurrió.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Fecha y hora en la que ocurrió el evento.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
