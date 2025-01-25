using Ardalis.GuardClauses;
using AutoSalePlaygroundAPI.Domain.DomainEvent;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public class Accessory : BaseEntity
    {
        public string Name { get; private set; } = null!;

        // Many-to-many relationship with Vehicles
        public virtual ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        protected Accessory() { }

        public Accessory(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name), "El nombre del accesorio no puede ser nulo o vacío.");

            Name = name;
        }

        public void UpdateName(string newName)
        {
            Guard.Against.NullOrWhiteSpace(newName, nameof(newName), "El nuevo nombre del accesorio no puede ser nulo o vacío.");

            Name = newName;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new AccessoryUpdatedDomainEvent(this.Id, newName));
        }
    }
}
