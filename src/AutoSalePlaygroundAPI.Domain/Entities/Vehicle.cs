using Ardalis.GuardClauses;
using AutoSalePlaygroundAPI.Domain.DomainEvent;
using AutoSalePlaygroundAPI.Domain.ValueObjects;

namespace AutoSalePlaygroundAPI.Domain.Entities
{
    /// <summary>
    /// Representa un vehículo en el dominio.
    /// Incluye información como la matrícula, el propietario, las especificaciones técnicas y los accesorios asociados.
    /// Hereda de <see cref="BaseEntity"/> para incorporar propiedades comunes y lógica de actualización.
    /// </summary>
    public class Vehicle : BaseEntity
    {
        /// <summary>
        /// Obtiene el número de placa del vehículo.
        /// </summary>
        public string LicensePlateNumber { get; protected set; } = null!;

        /// <summary>
        /// Obtiene el identificador del propietario del vehículo.
        /// </summary>
        public int OwnerId { get; protected set; }

        /// <summary>
        /// Obtiene el propietario del vehículo.
        /// </summary>
        public virtual Owner Owner { get; protected set; } = null!;

        /// <summary>
        /// Obtiene las especificaciones técnicas del vehículo, representadas como un value object.
        /// </summary>
        public ValueObjects.Specifications Specifications { get; set; } = null!;

        /// <summary>
        /// Colección de accesorios asociados al vehículo (relación muchos a muchos).
        /// </summary>
        public virtual ICollection<Accessory> Accessories { get; protected set; } = new List<Accessory>();

        /// <summary>
        /// Constructor protegido requerido por Entity Framework.
        /// </summary>
        protected Vehicle() { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Vehicle"/> con el identificador, número de placa y especificaciones.
        /// </summary>
        /// <param name="id">El identificador del vehículo.</param>
        /// <param name="licensePlateNumber">El número de placa del vehículo.</param>
        /// <param name="specifications">Las especificaciones técnicas del vehículo.</param>
        public Vehicle(int id, string licensePlateNumber, ValueObjects.Specifications specifications)
        {
            Id = id;
            LicensePlateNumber = licensePlateNumber;
            Specifications = specifications;
        }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="Vehicle"/> con el número de placa, propietario y especificaciones.
        /// </summary>
        /// <param name="licensePlateNumber">El número de placa del vehículo.</param>
        /// <param name="owner">El propietario del vehículo.</param>
        /// <param name="specifications">Las especificaciones técnicas del vehículo.</param>
        /// <exception cref="ArgumentException">
        /// Se lanza si <paramref name="licensePlateNumber"/> es nulo o vacío, o si <paramref name="owner"/> o <paramref name="specifications"/> son nulos.
        /// </exception>
        public Vehicle(string licensePlateNumber, Owner owner, ValueObjects.Specifications specifications)
        {
            Guard.Against.NullOrWhiteSpace(licensePlateNumber, nameof(licensePlateNumber), "El número de placa no puede ser nulo o vacío.");
            Guard.Against.Null(owner, nameof(owner), "El propietario no puede ser nulo.");
            Guard.Against.Null(specifications, nameof(specifications), "Las especificaciones no pueden ser nulas.");

            LicensePlateNumber = licensePlateNumber;
            Owner = owner;
            OwnerId = owner.Id;
            Specifications = specifications;
        }

        /// <summary>
        /// Cambia el propietario del vehículo.
        /// Actualiza el identificador y la referencia al nuevo propietario, marca la entidad como actualizada
        /// y lanza un evento de dominio para auditoría.
        /// </summary>
        /// <param name="newOwner">El nuevo propietario del vehículo.</param>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="newOwner"/> es nulo.</exception>
        public void ChangeOwner(Owner newOwner)
        {
            Guard.Against.Null(newOwner, nameof(newOwner), "El nuevo propietario no puede ser nulo.");

            var oldOwnerId = this.OwnerId;
            this.Owner = newOwner;
            this.OwnerId = newOwner.Id;
            MarkUpdated();

            // Lanza un evento de dominio para auditoría.
            DomainEvents.Raise(new VehicleOwnerChangedDomainEvent(this.Id, oldOwnerId, newOwner.Id));
        }

        /// <summary>
        /// Actualiza el número de placa del vehículo.
        /// Marca la entidad como actualizada y lanza un evento de dominio para auditoría.
        /// </summary>
        /// <param name="newLicensePlateNumber">El nuevo número de placa.</param>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="newLicensePlateNumber"/> es nulo o vacío.</exception>
        public void UpdateLicensePlate(string newLicensePlateNumber)
        {
            Guard.Against.NullOrWhiteSpace(newLicensePlateNumber, nameof(newLicensePlateNumber), "El nuevo número de placa no puede ser nulo o vacío.");

            var oldPlate = this.LicensePlateNumber;
            this.LicensePlateNumber = newLicensePlateNumber;
            MarkUpdated();

            // Lanza un evento de dominio para auditoría.
            DomainEvents.Raise(new VehicleLicensePlateUpdatedDomainEvent(this.Id, oldPlate, newLicensePlateNumber));
        }

        /// <summary>
        /// Agrega un accesorio al vehículo.
        /// Marca la entidad como actualizada y lanza un evento de dominio para auditoría.
        /// </summary>
        /// <param name="accessory">El accesorio a agregar.</param>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="accessory"/> es nulo.</exception>
        public void AddAccessory(Accessory accessory)
        {
            Guard.Against.Null(accessory, nameof(accessory), "Se intentó agregar un accesorio nulo.");
            Accessories.Add(accessory);
            MarkUpdated();

            // Lanza un evento de dominio para auditoría.
            DomainEvents.Raise(new VehicleAccessoryAddedDomainEvent(this.Id, accessory.Id));
        }

        /// <summary>
        /// Agrega una lista de accesorios al vehículo.
        /// Itera sobre cada accesorio y lo agrega individualmente.
        /// </summary>
        /// <param name="accessories">La lista de accesorios a agregar.</param>
        /// <exception cref="ArgumentNullException">Se lanza si la lista <paramref name="accessories"/> es nula.</exception>
        public void AddAccessories(List<Accessory> accessories)
        {
            Guard.Against.Null(accessories, nameof(accessories), "La lista de accesorios no puede ser nula.");
            foreach (var accessory in accessories)
            {
                AddAccessory(accessory);
            }
        }

        /// <summary>
        /// Actualiza las especificaciones técnicas del vehículo.
        /// Crea un nuevo value object <see cref="Specifications"/> con los nuevos valores,
        /// marca la entidad como actualizada y lanza un evento de dominio para auditoría.
        /// </summary>
        /// <param name="fuelType">El nuevo tipo de combustible.</param>
        /// <param name="engineDisplacement">La nueva cilindrada del motor.</param>
        /// <param name="horsepower">La nueva potencia del motor.</param>
        public void UpdateSpecifications(string fuelType, int engineDisplacement, int horsepower)
        {
            Guard.Against.NullOrWhiteSpace(fuelType, nameof(fuelType), "El tipo de combustible no puede ser nulo o vacío.");
            Guard.Against.NegativeOrZero(engineDisplacement, nameof(engineDisplacement), "La cilindrada del motor debe ser mayor que cero.");
            Guard.Against.NegativeOrZero(horsepower, nameof(horsepower), "La potencia del motor debe ser mayor que cero.");

            var oldSpecs = this.Specifications;
            this.Specifications = new ValueObjects.Specifications(fuelType, engineDisplacement, horsepower);
            MarkUpdated();

            // Lanza un evento de dominio para auditoría.
            DomainEvents.Raise(new VehicleSpecificationsUpdatedDomainEvent(
                this.Id,
                oldSpecs.FuelType, oldSpecs.EngineDisplacement, oldSpecs.Horsepower,
                fuelType, engineDisplacement, horsepower));
        }

        /// <summary>
        /// Actualiza el tipo de combustible en las especificaciones del vehículo.
        /// Marca la entidad como actualizada.
        /// </summary>
        /// <param name="newFuelType">El nuevo tipo de combustible.</param>
        public void UpdateFuelType(string newFuelType)
        {
            Guard.Against.NullOrWhiteSpace(newFuelType, nameof(newFuelType), "El nuevo tipo de combustible no puede ser nulo o vacío.");
            var oldFuelType = this.Specifications.FuelType;
            Specifications.UpdateFuelType(newFuelType);
            MarkUpdated();
            // Se puede lanzar un evento de dominio si se requiere.
            // DomainEvents.Raise(new VehicleFuelTypeUpdatedDomainEvent(this.Id, oldFuelType, newFuelType));
        }

        /// <summary>
        /// Actualiza la cilindrada del motor en las especificaciones del vehículo.
        /// Marca la entidad como actualizada.
        /// </summary>
        /// <param name="newEngineDisplacement">La nueva cilindrada del motor.</param>
        public void UpdateEngineDisplacement(int newEngineDisplacement)
        {
            Guard.Against.NegativeOrZero(newEngineDisplacement, nameof(newEngineDisplacement), "La nueva cilindrada del motor debe ser mayor que cero.");
            var oldEngineDisplacement = this.Specifications.EngineDisplacement;
            Specifications.UpdateEngineDisplacement(newEngineDisplacement);
            MarkUpdated();
            // Se puede lanzar un evento de dominio si se requiere.
            // DomainEvents.Raise(new VehicleEngineDisplacementUpdatedDomainEvent(this.Id, oldEngineDisplacement, newEngineDisplacement));
        }

        /// <summary>
        /// Actualiza la potencia del motor en las especificaciones del vehículo.
        /// Marca la entidad como actualizada.
        /// </summary>
        /// <param name="newHorsepower">La nueva potencia del motor.</param>
        public void UpdateHorsepower(int newHorsepower)
        {
            Guard.Against.NegativeOrZero(newHorsepower, nameof(newHorsepower), "La nueva potencia del motor debe ser mayor que cero.");
            var oldHorsepower = this.Specifications.Horsepower;
            Specifications.UpdateHorsepower(newHorsepower);
            MarkUpdated();
            // Se puede lanzar un evento de dominio si se requiere.
            // DomainEvents.Raise(new VehicleHorsepowerUpdatedDomainEvent(this.Id, oldHorsepower, newHorsepower));
        }
    }
}
