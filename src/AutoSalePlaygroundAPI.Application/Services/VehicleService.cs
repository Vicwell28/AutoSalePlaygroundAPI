using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.CrossCutting.Enum;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con vehículos.
    /// Este servicio utiliza especificaciones para filtrar y paginar resultados y delega el acceso a datos en un repositorio genérico.
    /// Además, implementa métodos para actualizaciones completas, parciales y en bloque, y se integra con un pipeline de transacciones centralizado.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia de <see cref="VehicleService"/>.
    /// </remarks>
    /// <param name="vehicleRepository">Repositorio para la entidad <see cref="Vehicle"/>.</param>
    /// <param name="accessoryRepository">Repositorio para la entidad <see cref="Accessory"/>.</param>
    /// <exception cref="ArgumentNullException">Se lanza si alguno de los repositorios es <c>null</c>.</exception>
    public class VehicleService(IRepository<Vehicle> vehicleRepository, IRepository<Accessory> accessoryRepository) : IVehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository = vehicleRepository
            ?? throw new ArgumentNullException(nameof(vehicleRepository));

        private readonly IRepository<Accessory> _accessoryRepository = accessoryRepository
            ?? throw new ArgumentNullException(nameof(accessoryRepository));

        /// <inheritdoc />
        public async Task<List<Vehicle>> GetActiveVehiclesByOwnerAsync(int ownerId)
        {
            // Se utiliza una especificación para filtrar vehículos activos según el propietario.
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await _vehicleRepository.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
        {
            // Se utiliza una especificación genérica para filtrar por Id.
            var spec = new GenericVehicleSpec(v => v.Id == vehicleId);
            return await _vehicleRepository.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<int>> GetVehicleIdsActiveByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            // Se utiliza la sobrecarga que permite proyectar el resultado para obtener solo el Id.
            return await _vehicleRepository.ListAsync<int>(spec, v => v.Id);
        }

        /// <inheritdoc />
        public async Task<int?> GetVehicleIdActiveByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await _vehicleRepository.FirstOrDefaultAsync(spec, v => v.Id);
        }

        /// <inheritdoc />
        public async Task<(List<Vehicle> Vehicles, int TotalCount)> GetActiveVehiclesByOwnerPagedAsync(
            int ownerId,
            int pageNumber,
            int pageSize,
            VehicleSortByEnum vehicleSortByEnum,
            OrderByEnum orderByEnum)
        {
            var spec = new VehicleActiveByOwnerPagedSpec(ownerId, pageNumber, pageSize, vehicleSortByEnum, orderByEnum);
            return await _vehicleRepository.ListPaginatedAsync(spec);
        }

        /// <inheritdoc />
        public async Task<Vehicle> AddNewVehicleAsync(string licensePlateNumber, Owner owner, Specifications specifications)
        {
            var vehicle = new Vehicle(licensePlateNumber, owner, specifications);
            await _vehicleRepository.AddAsync(vehicle);
            return vehicle;
        }

        /// <inheritdoc />
        public async Task UpdateVehicleLicensePlateAsync(int vehicleId, string newLicensePlate)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado.");
            }

            // Actualiza la placa utilizando la lógica de dominio (que puede generar validaciones o eventos).
            vehicle.UpdateLicensePlate(newLicensePlate);
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task ChangeVehicleOwnerAsync(int vehicleId, Owner newOwner)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado.");
            }

            // Cambia el propietario y actualiza la entidad.
            vehicle.ChangeOwner(newOwner);
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task AddVehicleAccessories(int vehicleId, List<int> accessories)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado.");
            }

            // Se utiliza una especificación para obtener los accesorios cuyos Ids están en la lista.
            var accessorySpec = new GenericAccessorySpec(x => accessories.Contains(x.Id));
            var accesoryEntities = await _accessoryRepository.ListAsync(accessorySpec);

            // Se agregan cada uno de los accesorios al vehículo.
            foreach (var accessory in accesoryEntities)
            {
                vehicle.AddAccessory(accessory);
            }

            await _vehicleRepository.UpdateAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task ExecuteVehicleUpdateAsync(
            int vehicleId,
            string newLicensePlate,
            string fuelType,
            int engineDisplacement,
            int horsepower)
        {
            // Realiza una actualización en bloque utilizando ExecuteUpdateAsync, 
            // la cual actualiza directamente en la base de datos sin cargar la entidad completa en memoria.
            await _vehicleRepository.ExecuteUpdateAsync(
                predicate: p => p.Id == vehicleId,
                updateExpression: set => set
                    .SetProperty(p => p.LicensePlateNumber, newLicensePlate)
                    .SetProperty(p => p.Specifications.FuelType, fuelType)
                    .SetProperty(p => p.Specifications.EngineDisplacement, engineDisplacement)
                    .SetProperty(p => p.Specifications.Horsepower, horsepower)
            );
        }

        /// <inheritdoc />
        public async Task DeleteVehicleByIdAsync(int vehicleId)
        {
            // Elimina el vehículo mediante ExecuteDeleteAsync, generando una sentencia SQL DELETE que actúa en bloque.
            await _vehicleRepository.ExecuteDeleteAsync(v => v.Id == vehicleId);
        }

        /// <inheritdoc />
        public Task PartialVehicleUpdateAsync(Vehicle vehicle)
        {
            // Adjunta la entidad al contexto para que EF Core la rastree.
            _vehicleRepository.DbContext.Attach(vehicle);
            var vehicleEntry = _vehicleRepository.DbContext.Entry(vehicle);


            // Actualiza la placa si se ha proporcionado un nuevo valor.
            if (vehicle.LicensePlateNumber != null)
            {
                vehicle.UpdateLicensePlate(vehicle.LicensePlateNumber);
                _vehicleRepository.DbContext.Entry(vehicle)
                    .Property(p => p.LicensePlateNumber).IsModified = true;
            }

            // Actualiza el tipo de combustible si se ha proporcionado un valor.
            // Actualiza la cilindrada si el valor es distinto de cero
            // Actualiza la potencia si el valor es distinto de cero
            if (vehicle.Specifications.FuelType != null && vehicle.Specifications.EngineDisplacement != 0 && vehicle.Specifications.Horsepower != 0)
            {
                vehicle.UpdateFuelType(vehicle.Specifications.FuelType);
                vehicle.UpdateEngineDisplacement(vehicle.Specifications.EngineDisplacement);
                vehicle.UpdateHorsepower(vehicle.Specifications.Horsepower);

                // Para la propiedad FuelType:
                vehicleEntry.Reference(v => v.Specifications).TargetEntry
                    .Property(s => s.FuelType).IsModified = true;

                // Para la propiedad EngineDisplacement:
                vehicleEntry.Reference(v => v.Specifications).TargetEntry
                    .Property(s => s.EngineDisplacement).IsModified = true;

                // Para la propiedad Horsepower:
                vehicleEntry.Reference(v => v.Specifications).TargetEntry
                    .Property(s => s.Horsepower).IsModified = true;
            }

            // La persistencia (SaveChanges) se gestiona de forma centralizada (por ejemplo, mediante Unit of Work).
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task PartialVehiclesBulkUpdateAsync(IEnumerable<Vehicle> vehicles)
        {
            // Itera sobre cada vehículo de la colección para actualizar de forma parcial.
            foreach (var vehicle in vehicles)
            {
                _vehicleRepository.DbContext.Attach(vehicle);

                if (vehicle.LicensePlateNumber != null)
                {
                    vehicle.UpdateLicensePlate(vehicle.LicensePlateNumber);
                    _vehicleRepository.DbContext.Entry(vehicle)
                        .Property(p => p.LicensePlateNumber).IsModified = true;
                }

                if (vehicle.Specifications.FuelType != null)
                {
                    vehicle.UpdateFuelType(vehicle.Specifications.FuelType);
                    _vehicleRepository.DbContext.Entry(vehicle)
                        .Property(p => p.Specifications.FuelType).IsModified = true;
                }

                if (vehicle.Specifications.EngineDisplacement != 0)
                {
                    vehicle.UpdateEngineDisplacement(vehicle.Specifications.EngineDisplacement);
                    _vehicleRepository.DbContext.Entry(vehicle)
                        .Property(p => p.Specifications.EngineDisplacement).IsModified = true;
                }

                if (vehicle.Specifications.Horsepower != 0)
                {
                    vehicle.UpdateHorsepower(vehicle.Specifications.Horsepower);
                    _vehicleRepository.DbContext.Entry(vehicle)
                        .Property(p => p.Specifications.Horsepower).IsModified = true;
                }
            }

            // La persistencia de los cambios se realiza externamente en una única transacción.
            return Task.CompletedTask;
        }
    }
}
