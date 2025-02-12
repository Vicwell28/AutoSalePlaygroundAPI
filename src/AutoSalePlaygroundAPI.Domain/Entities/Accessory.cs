using Ardalis.GuardClauses;
using AutoSalePlaygroundAPI.Domain.DomainEvent;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    /// <summary>
    /// Representa un accesorio que puede estar asociado a uno o varios vehículos.
    /// Hereda de <see cref="BaseEntity"/> para incluir propiedades comunes como <see cref="Id"/> y <see cref="CreatedAt"/>.
    /// </summary>
    public class Accessory : BaseEntity
    {
        /// <summary>
        /// Obtiene el nombre del accesorio.
        /// </summary>
        public string Name { get; private set; } = null!;

        /// <summary>
        /// Colección de vehículos asociados a este accesorio (relación muchos a muchos).
        /// </summary>
        public virtual ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        /// <summary>
        /// Constructor protegido requerido por Entity Framework.
        /// </summary>
        protected Accessory() { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Accessory"/> con el nombre especificado.
        /// </summary>
        /// <param name="name">El nombre del accesorio.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="name"/> es nulo o vacío.</exception>
        public Accessory(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name), "El nombre del accesorio no puede ser nulo o vacío.");
            Name = name;
        }

        /// <summary>
        /// Actualiza el nombre del accesorio.
        /// Marca la entidad como actualizada y lanza un evento de dominio para auditoría.
        /// </summary>
        /// <param name="newName">El nuevo nombre del accesorio.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="newName"/> es nulo o vacío.</exception>
        public void UpdateName(string newName)
        {
            Guard.Against.NullOrWhiteSpace(newName, nameof(newName), "El nuevo nombre del accesorio no puede ser nulo o vacío.");
            Name = newName;
            MarkUpdated();

            // Lanza un evento de dominio para auditoría.
            DomainEvents.Raise(new AccessoryUpdatedDomainEvent(this.Id, newName));
        }
    }
}
