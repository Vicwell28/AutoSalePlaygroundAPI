using Ardalis.GuardClauses;
using AutoSalePlaygroundAPI.Domain.DomainEvent;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    /// <summary>
    /// Representa al propietario de uno o varios vehículos.
    /// Hereda de <see cref="BaseEntity"/> para incorporar propiedades comunes y lógica de actualización.
    /// </summary>
    public class Owner : BaseEntity
    {
        /// <summary>
        /// Obtiene el nombre del propietario.
        /// </summary>
        public string FirstName { get; private set; } = null!;

        /// <summary>
        /// Obtiene el apellido del propietario.
        /// </summary>
        public string LastName { get; private set; } = null!;

        /// <summary>
        /// Colección de vehículos asociados a este propietario (relación uno a muchos).
        /// </summary>
        public virtual ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        /// <summary>
        /// Constructor protegido requerido por Entity Framework.
        /// </summary>
        protected Owner() { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Owner"/> con el nombre y apellido especificados.
        /// </summary>
        /// <param name="firstName">El nombre del propietario.</param>
        /// <param name="lastName">El apellido del propietario.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="firstName"/> o <paramref name="lastName"/> son nulos o vacíos.</exception>
        public Owner(string firstName, string lastName)
        {
            Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName), "El nombre no puede ser nulo o vacío.");
            Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName), "El apellido no puede ser nulo o vacío.");
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Actualiza el nombre y apellido del propietario.
        /// Marca la entidad como actualizada y lanza un evento de dominio para auditoría.
        /// </summary>
        /// <param name="newFirstName">El nuevo nombre.</param>
        /// <param name="newLastName">El nuevo apellido.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="newFirstName"/> o <paramref name="newLastName"/> son nulos o vacíos.</exception>
        public void UpdateName(string newFirstName, string newLastName)
        {
            Guard.Against.NullOrWhiteSpace(newFirstName, nameof(newFirstName), "El nuevo nombre no puede ser nulo o vacío.");
            Guard.Against.NullOrWhiteSpace(newLastName, nameof(newLastName), "El nuevo apellido no puede ser nulo o vacío.");
            FirstName = newFirstName;
            LastName = newLastName;
            MarkUpdated();

            // Lanza un evento de dominio para auditoría.
            DomainEvents.Raise(new OwnerUpdatedDomainEvent(this.Id, newFirstName, newLastName));
        }
    }
}
