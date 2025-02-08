using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.CrossCutting.Enum;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con vehículos.
    /// Utiliza especificaciones para filtrar y paginar resultados y delega el acceso a datos en el repositorio.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly IRepository<Accessory> _accessoryRepository;

        public VehicleService(IRepository<Vehicle> vehicleRepository, IRepository<Accessory> accessoryRepository)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _accessoryRepository = accessoryRepository ?? throw new ArgumentNullException(nameof(accessoryRepository));
        }

        /// <summary>
        /// Obtiene la lista de vehículos activos asociados a un propietario.
        /// </summary>
        public async Task<List<Vehicle>> GetActiveVehiclesByOwnerAsync(int ownerId)
        {
            // Se usa una especificación para filtrar vehículos activos por dueño.
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await _vehicleRepository.ListAsync(spec);
        }

        /// <summary>
        /// Obtiene un vehículo a partir de su Id.
        /// </summary>
        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
        {
            // Se utiliza una especificación genérica que filtra por Id.
            var spec = new GenericVehicleSpec(v => v.Id == vehicleId);
            return await _vehicleRepository.FirstOrDefaultAsync(spec);
        }

        /// <summary>
        /// Obtiene una lista de Ids de vehículos activos de un propietario.
        /// </summary>
        public async Task<List<int>> GetVehicleIdsActiveByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            // Se usa la sobrecarga que permite proyectar el resultado (en este caso, sólo el Id).
            return await _vehicleRepository.ListAsync<int>(spec, v => v.Id);
        }

        /// <summary>
        /// Obtiene el Id del primer vehículo activo de un propietario.
        /// </summary>
        public async Task<int?> GetVehicleIdActiveByOwnerAsync(int ownerId)
        {
            var spec = new VehicleActiveByOwnerSpec(ownerId);
            return await _vehicleRepository.FirstOrDefaultAsync(spec, v => v.Id);
        }

        /// <summary>
        /// Obtiene vehículos activos de un propietario de forma paginada.
        /// </summary>
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

        /// <summary>
        /// Agrega un nuevo vehículo.
        /// </summary>
        public async Task<Vehicle> AddNewVehicleAsync(string licensePlateNumber, Owner owner, Domain.ValueObjects.Specifications specifications)
        {
            var vehicle = new Vehicle(licensePlateNumber, owner, specifications);
            await _vehicleRepository.AddAsync(vehicle);
            return vehicle;
        }

        /// <summary>
        /// Actualiza la placa de un vehículo.
        /// </summary>
        public async Task UpdateVehicleLicensePlateAsync(int vehicleId, string newLicensePlate)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado.");
            }
            vehicle.UpdateLicensePlate(newLicensePlate);
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        /// <summary>
        /// Cambia el propietario de un vehículo.
        /// </summary>
        public async Task ChangeVehicleOwnerAsync(int vehicleId, Owner newOwner)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado.");
            }
            vehicle.ChangeOwner(newOwner);
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        public async Task AddVehicleAccessories(int vehicleId, List<int> accessories)
        {
            var vehicle = await GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado.");
            }

            var accessorySpec = new GenericAccessorySpec(x => accessories.Contains(x.Id));
            var accesoryEntityes = await _accessoryRepository.ListAsync(accessorySpec);

            foreach (var accessory in accesoryEntityes)
            {
                vehicle.AddAccessory(accessory);
            }

            await _vehicleRepository.UpdateAsync(vehicle);
        }
    }
}
