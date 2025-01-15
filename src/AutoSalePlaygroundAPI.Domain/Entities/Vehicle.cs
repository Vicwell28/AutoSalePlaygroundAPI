using Ardalis.GuardClauses;
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
        public virtual ICollection<Accessory> Accessories { get; private set; } = new List<Accessory>();

        private Vehicle() { }

        public Vehicle(string licensePlateNumber, Owner owner, Specifications specifications)
        {
            Guard.Against.NullOrWhiteSpace(licensePlateNumber, nameof(licensePlateNumber), "El número de placa no puede ser nulo o vacío.");
            Guard.Against.Null(owner, nameof(owner), "El propietario no puede ser nulo.");
            Guard.Against.Null(specifications, nameof(specifications), "Las especificaciones no pueden ser nulas.");

            LicensePlateNumber = licensePlateNumber;
            Owner = owner;
            OwnerId = owner.Id;
            Specifications = specifications;
        }

        public void ChangeOwner(Owner newOwner)
        {
            Guard.Against.Null(newOwner, nameof(newOwner), "El nuevo propietario no puede ser nulo.");

            var oldOwnerId = this.OwnerId;
            this.Owner = newOwner;
            this.OwnerId = newOwner.Id;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new VehicleOwnerChangedDomainEvent(this.Id, oldOwnerId, newOwner.Id));
        }

        public void UpdateLicensePlate(string newLicensePlateNumber)
        {
            Guard.Against.NullOrWhiteSpace(newLicensePlateNumber, nameof(newLicensePlateNumber), "El nuevo número de placa no puede ser nulo o vacío.");

            var oldPlate = this.LicensePlateNumber;
            this.LicensePlateNumber = newLicensePlateNumber;
            MarkUpdated();

            // Raise a domain event for auditing
            DomainEvents.Raise(new VehicleLicensePlateUpdatedDomainEvent(this.Id, oldPlate, newLicensePlateNumber));
        }

        public void AddAccessory(Accessory accessory)
        {
            Guard.Against.Null(accessory, nameof(accessory), "Se intentó agregar un accesorio nulo.");

            Accessories.Add(accessory);
            MarkUpdated();

            // Raise a domain event
            DomainEvents.Raise(new VehicleAccessoryAddedDomainEvent(this.Id, accessory.Id));
        }

        public void UpdateSpecifications(string fuelType, int engineDisplacement, int horsepower)
        {
            Guard.Against.NullOrWhiteSpace(fuelType, nameof(fuelType), "El tipo de combustible no puede ser nulo o vacío.");
            Guard.Against.NegativeOrZero(engineDisplacement, nameof(engineDisplacement), "La cilindrada del motor debe ser mayor que cero.");
            Guard.Against.NegativeOrZero(horsepower, nameof(horsepower), "La potencia del motor debe ser mayor que cero.");

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
