using AutoSalePlaygroundAPI.Domain.DomainEvent;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;

        // Relationship: One Owner -> Many Vehicles
        public virtual ICollection<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        private Owner() { }

        public Owner(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateName(string newFirstName, string newLastName)
        {
            FirstName = newFirstName;
            LastName = newLastName;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new OwnerUpdatedDomainEvent(this.Id, newFirstName, newLastName));
        }
    }
}
