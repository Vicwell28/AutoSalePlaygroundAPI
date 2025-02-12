namespace AutoSalePlaygroundAPI.Domain.Entities
{
    /// <summary>
    /// Clase base abstracta para todas las entidades del dominio.
    /// Proporciona propiedades comunes como <see cref="Id"/>, <see cref="CreatedAt"/>, <see cref="UpdatedAt"/> y gestión del estado activo/inactivo.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Obtiene el identificador único de la entidad.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Obtiene la fecha y hora en que la entidad fue creada.
        /// </summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Obtiene la fecha y hora en que la entidad fue actualizada por última vez.
        /// </summary>
        public DateTime? UpdatedAt { get; protected set; }

        /// <summary>
        /// Indica si la entidad está activa.
        /// </summary>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Inicializa una nueva instancia de la entidad asignando la fecha actual y marcándola como activa.
        /// </summary>
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Marca la entidad como actualizada, asignando la fecha y hora actual a <see cref="UpdatedAt"/>.
        /// </summary>
        public virtual void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Desactiva la entidad, marcándola como inactiva y actualizando la fecha de modificación.
        /// </summary>
        public virtual void Deactivate()
        {
            IsActive = false;
            MarkUpdated();
        }

        /// <summary>
        /// Activa la entidad, marcándola como activa y actualizando la fecha de modificación.
        /// </summary>
        public virtual void Activate()
        {
            IsActive = true;
            MarkUpdated();
        }
    }
}
