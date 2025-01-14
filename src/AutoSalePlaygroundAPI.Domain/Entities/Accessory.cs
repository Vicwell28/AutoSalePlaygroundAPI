using AutoSalePlaygroundAPI.Domain.DomainEvent;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public class Accessory : BaseEntity, IEntity
    {
        public string Name { get; private set; } = null!;

        // Many-to-many relationship with Vehicles
        public virtual ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        private Accessory() { }

        public Accessory(string name)
        {
            Name = name;
        }

        public void UpdateName(string newName)
        {
            Name = newName;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new AccessoryUpdatedDomainEvent(this.Id, newName));
        }
    }
}