using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.ValueObjects;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato del servicio de aplicación para gestionar operaciones relacionadas con vehículos.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Obtiene la lista de vehículos activos asociados a un propietario.
        /// </summary>
        Task<List<Vehicle>> GetActiveVehiclesByOwnerAsync(int ownerId);

        /// <summary>
        /// Obtiene un vehículo a partir de su Id.
        /// </summary>
        Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);

        /// <summary>
        /// Obtiene una lista de Ids de vehículos activos de un propietario.
        /// </summary>
        Task<List<int>> GetVehicleIdsActiveByOwnerAsync(int ownerId);

        /// <summary>
        /// Obtiene el Id del primer vehículo activo de un propietario.
        /// </summary>
        Task<int?> GetVehicleIdActiveByOwnerAsync(int ownerId);

        /// <summary>
        /// Obtiene vehículos activos de un propietario de forma paginada.
        /// </summary>
        Task<(List<Vehicle> Vehicles, int TotalCount)> GetActiveVehiclesByOwnerPagedAsync(int ownerId, int pageNumber, int pageSize);

        /// <summary>
        /// Agrega un nuevo vehículo.
        /// </summary>
        Task<Vehicle> AddNewVehicleAsync(string licensePlateNumber, Owner owner, Domain.ValueObjects.Specifications specifications);

        /// <summary>
        /// Actualiza la placa de un vehículo.
        /// </summary>
        Task UpdateVehicleLicensePlateAsync(int vehicleId, string newLicensePlate);

        /// <summary>
        /// Cambia el propietario de un vehículo.
        /// </summary>
        Task ChangeVehicleOwnerAsync(int vehicleId, Owner newOwner);

        /// <summary>
        /// Agrega accesorios a un vehículo.
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="accessories"></param>
        /// <returns></returns>
        Task AddVehicleAccessories(int vehicleId, List<Accessory> accessories);
    }
}