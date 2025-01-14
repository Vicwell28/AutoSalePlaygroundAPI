using AutoSalePlaygroundAPI.Domain.DomainEvent;
using AutoSalePlaygroundAPI.Domain.ValueObjects;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string LicensePlateNumber { get; private set; } = null!;
        public int OwnerId { get; private set; }
        public virtual Owner Owner { get; private set; } = null!;
        public Specifications Specifications { get; private set; } = null!;

        // Many-to-many relationship with Accessories
        public virtual ICollection<Accessory> Accessories { get; private set; } = new List<Accessory>();

        private Vehicle() { }

        public Vehicle(string licensePlateNumber, Owner owner, Specifications specifications)
        {
            LicensePlateNumber = licensePlateNumber;
            Owner = owner;
            OwnerId = owner.Id;
            Specifications = specifications;
        }

        public void ChangeOwner(Owner newOwner)
        {
            var oldOwnerId = this.OwnerId;
            this.Owner = newOwner;
            this.OwnerId = newOwner.Id;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new VehicleOwnerChangedDomainEvent(this.Id, oldOwnerId, newOwner.Id));
        }

        public void UpdateLicensePlate(string newLicensePlateNumber)
        {
            var oldPlate = this.LicensePlateNumber;
            this.LicensePlateNumber = newLicensePlateNumber;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new VehicleLicensePlateUpdatedDomainEvent(this.Id, oldPlate, newLicensePlateNumber));
        }

        public void AddAccessory(Accessory accessory)
        {
            if (accessory == null)
                throw new ArgumentNullException(nameof(accessory));

            Accessories.Add(accessory);
            MarkUpdated();

            // Raise a domain event
            DomainEvents.Raise(new VehicleAccessoryAddedDomainEvent(this.Id, accessory.Id));
        }

        public void UpdateSpecifications(string fuelType, int engineDisplacement, int horsepower)
        {
            var oldSpecs = this.Specifications;
            this.Specifications = new Specifications(fuelType, engineDisplacement, horsepower);
            MarkUpdated();

            // Raise a domain event
            DomainEvents.Raise(new VehicleSpecificationsUpdatedDomainEvent(
                this.Id,
                oldSpecs.FuelType, oldSpecs.EngineDisplacement, oldSpecs.Horsepower,
                fuelType, engineDisplacement, horsepower));
        }
    }
}