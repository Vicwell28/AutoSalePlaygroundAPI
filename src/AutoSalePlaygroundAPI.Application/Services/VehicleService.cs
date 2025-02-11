using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Enum;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Domain.ValueObjects;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con vehículos.
    /// Este servicio extiende las operaciones genéricas definidas en <see cref="BaseService{Vehicle}"/>
    /// y agrega métodos específicos para la entidad <see cref="Vehicle"/>.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia de <see cref="VehicleService"/>.
    /// </remarks>
    /// <param name="vehicleRepository">Repositorio para la entidad <see cref="Vehicle"/>.</param>
    /// <param name="accessoryRepository">Repositorio para la entidad <see cref="Accessory"/>.</param>
    /// <exception cref="ArgumentNullException">Se lanza si alguno de los repositorios es null.</exception>
    public class VehicleService(IRepository<Vehicle> vehicleRepository, IRepository<Accessory> accessoryRepository)
        : BaseService<Vehicle>(vehicleRepository), IVehicleService
    {
        // Repositorio específico para la entidad Accessory, utilizado en operaciones particulares.
        private readonly IRepository<Accessory> _accessoryRepository = accessoryRepository 
            ?? throw new ArgumentNullException(nameof(accessoryRepository));

        /// <inheritdoc />
        public async Task<List<Vehicle>> GetActiveVehiclesByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await base.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
        {
            var spec = new GenericVehicleSpec(v => v.Id == vehicleId);
            return await base.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<int>> GetVehicleIdsActiveByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await _repository.ListAsync<int>(spec, v => v.Id);
        }

        /// <inheritdoc />
        public async Task<int?> GetVehicleIdActiveByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await _repository.FirstOrDefaultAsync(spec, v => v.Id);
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
            return await _repository.ListPaginatedAsync(spec);
        }

        /// <inheritdoc />
        public async Task<Vehicle> AddNewVehicleAsync(string licensePlateNumber, Owner owner, Specifications specifications)
        {
            var vehicle = new Vehicle(licensePlateNumber, owner, specifications);
            await base.AddAsync(vehicle);
            return vehicle;
        }

        /// <inheritdoc />
        public async Task UpdateVehicleLicensePlateAsync(int vehicleId, string newLicensePlate)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new NotFoundException("Vehículo no encontrado.");
            }

            // Se actualiza la placa utilizando la lógica de dominio.
            vehicle.UpdateLicensePlate(newLicensePlate);
            await base.UpdateAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task ChangeVehicleOwnerAsync(int vehicleId, Owner newOwner)
        {
            // Se obtiene el vehículo y se actualiza el propietario.
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new NotFoundException("Vehículo no encontrado.");
            }

            vehicle.ChangeOwner(newOwner);
            await base.UpdateAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task AddVehicleAccessories(int vehicleId, List<int> accessories)
        {
            // Se obtiene el vehículo al que se agregarán accesorios.
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new NotFoundException("Vehículo no encontrado.");
            }

            // Se crea una especificación para obtener los accesorios cuyos Ids se encuentran en la lista.
            var accessorySpec = new GenericAccessorySpec(x => accessories.Contains(x.Id));
            var accessoryEntities = await _accessoryRepository.ListAsync(accessorySpec);

            // Se agregan cada uno de los accesorios al vehículo.
            foreach (var accessory in accessoryEntities)
            {
                vehicle.AddAccessory(accessory);
            }

            // Se actualiza el vehículo utilizando el método genérico de la clase base.
            await base.UpdateAsync(vehicle);
        }

        /// <inheritdoc />
        public async Task ExecuteVehicleUpdateAsync(
            int vehicleId,
            string newLicensePlate,
            string fuelType,
            int engineDisplacement,
            int horsepower)
        {
            // Realiza una actualización en bloque directamente en la base de datos sin cargar la entidad.
            await _repository.ExecuteUpdateAsync(
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
            await _repository.ExecuteDeleteAsync(v => v.Id == vehicleId);
        }

        /// <inheritdoc />
        public Task PartialVehicleUpdateAsync(Vehicle vehicle)
        {
            // Actualización parcial de una entidad Vehicle.
            // Se adjunta la entidad al DbContext y se marcan las propiedades modificadas.
            _repository.DbContext.Attach(vehicle);
            var vehicleEntry = _repository.DbContext.Entry(vehicle);

            if (!string.IsNullOrEmpty(vehicle.LicensePlateNumber))
            {
                vehicle.UpdateLicensePlate(vehicle.LicensePlateNumber);
                vehicleEntry.Property(v => v.LicensePlateNumber).IsModified = true;
            }

            var specsEntry = vehicleEntry.Reference(v => v.Specifications).TargetEntry;
            if (specsEntry != null)
            {
                if (!string.IsNullOrEmpty(vehicle.Specifications.FuelType))
                {
                    vehicle.UpdateFuelType(vehicle.Specifications.FuelType);
                    specsEntry.Property(s => s.FuelType).IsModified = true;
                }
                if (vehicle.Specifications.EngineDisplacement != 0)
                {
                    vehicle.UpdateEngineDisplacement(vehicle.Specifications.EngineDisplacement);
                    specsEntry.Property(s => s.EngineDisplacement).IsModified = true;
                }
                if (vehicle.Specifications.Horsepower != 0)
                {
                    vehicle.UpdateHorsepower(vehicle.Specifications.Horsepower);
                    specsEntry.Property(s => s.Horsepower).IsModified = true;
                }
            }

            // La persistencia se gestiona de forma centralizada en el pipeline.
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task PartialVehiclesBulkUpdateAsync(IEnumerable<Vehicle> vehicles)
        {
            // Actualización parcial de un conjunto de vehículos.
            foreach (var vehicle in vehicles)
            {
                _repository.DbContext.Attach(vehicle);
                var vehicleEntry = _repository.DbContext.Entry(vehicle);

                if (!string.IsNullOrEmpty(vehicle.LicensePlateNumber))
                {
                    vehicle.UpdateLicensePlate(vehicle.LicensePlateNumber);
                    vehicleEntry.Property(v => v.LicensePlateNumber).IsModified = true;
                }

                var specsEntry = vehicleEntry.Reference(v => v.Specifications).TargetEntry;
                if (specsEntry != null)
                {
                    if (!string.IsNullOrEmpty(vehicle.Specifications.FuelType))
                    {
                        vehicle.UpdateFuelType(vehicle.Specifications.FuelType);
                        specsEntry.Property(s => s.FuelType).IsModified = true;
                    }
                    if (vehicle.Specifications.EngineDisplacement != 0)
                    {
                        vehicle.UpdateEngineDisplacement(vehicle.Specifications.EngineDisplacement);
                        specsEntry.Property(s => s.EngineDisplacement).IsModified = true;
                    }
                    if (vehicle.Specifications.Horsepower != 0)
                    {
                        vehicle.UpdateHorsepower(vehicle.Specifications.Horsepower);
                        specsEntry.Property(s => s.Horsepower).IsModified = true;
                    }
                }
            }

            // La persistencia se gestiona de forma centralizada.
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task<(List<Vehicle> Vehicles, int TotalCount)> VehicleDynamicSort(List<SortCriteriaDto> sortCriteria, int pageNumber, int pageSize)
        {
            var spec = new VehicleDynamicSortSpec(sortCriteria, pageNumber, pageSize);
            return await _repository.ListPaginatedAsync(spec);
        }
    }
}
