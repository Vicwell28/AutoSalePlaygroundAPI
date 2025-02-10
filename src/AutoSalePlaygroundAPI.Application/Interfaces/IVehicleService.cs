using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Enum;
using AutoSalePlaygroundAPI.Domain.ValueObjects;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato del servicio de aplicación para gestionar operaciones relacionadas con vehículos.
    /// Proporciona métodos para obtener, agregar, actualizar (completo, parcial o en bloque) y eliminar vehículos,
    /// así como para administrar operaciones relacionadas (por ejemplo, agregar accesorios).
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Obtiene la lista de vehículos activos asociados a un propietario.
        /// </summary>
        /// <param name="ownerId">Identificador del propietario.</param>
        /// <returns>Lista de vehículos activos del propietario.</returns>
        Task<List<Vehicle>> GetActiveVehiclesByOwnerAsync(int ownerId);

        /// <summary>
        /// Obtiene un vehículo a partir de su identificador.
        /// </summary>
        /// <param name="vehicleId">Identificador del vehículo.</param>
        /// <returns>El vehículo encontrado o <c>null</c> si no existe.</returns>
        Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);

        /// <summary>
        /// Obtiene una lista de identificadores de vehículos activos de un propietario.
        /// </summary>
        /// <param name="ownerId">Identificador del propietario.</param>
        /// <returns>Lista de identificadores de vehículos activos.</returns>
        Task<List<int>> GetVehicleIdsActiveByOwnerAsync(int ownerId);

        /// <summary>
        /// Obtiene el identificador del primer vehículo activo de un propietario.
        /// </summary>
        /// <param name="ownerId">Identificador del propietario.</param>
        /// <returns>El identificador del primer vehículo activo o <c>null</c> si no existe.</returns>
        Task<int?> GetVehicleIdActiveByOwnerAsync(int ownerId);

        /// <summary>
        /// Obtiene vehículos activos de un propietario de forma paginada.
        /// </summary>
        /// <param name="ownerId">Identificador del propietario.</param>
        /// <param name="pageNumber">Número de página a obtener.</param>
        /// <param name="pageSize">Cantidad de registros por página.</param>
        /// <param name="vehicleSortByEnum">Criterio de ordenamiento para vehículos.</param>
        /// <param name="orderByEnum">Indica si el orden es ascendente o descendente.</param>
        /// <returns>
        /// Una tupla que contiene la lista de vehículos de la página solicitada y el total de registros.
        /// </returns>
        Task<(List<Vehicle> Vehicles, int TotalCount)> GetActiveVehiclesByOwnerPagedAsync(
            int ownerId,
            int pageNumber,
            int pageSize,
            VehicleSortByEnum vehicleSortByEnum,
            OrderByEnum orderByEnum);

        /// <summary>
        /// Agrega un nuevo vehículo.
        /// </summary>
        /// <param name="licensePlateNumber">Número de placa del vehículo.</param>
        /// <param name="owner">Entidad <see cref="Owner"/> que será el propietario del vehículo.</param>
        /// <param name="specifications">Especificaciones del vehículo.</param>
        /// <returns>El vehículo agregado.</returns>
        Task<Vehicle> AddNewVehicleAsync(string licensePlateNumber, Owner owner, Specifications specifications);

        /// <summary>
        /// Actualiza la placa de un vehículo.
        /// </summary>
        /// <param name="vehicleId">Identificador del vehículo a actualizar.</param>
        /// <param name="newLicensePlate">Nuevo número de placa.</param>
        Task UpdateVehicleLicensePlateAsync(int vehicleId, string newLicensePlate);

        /// <summary>
        /// Cambia el propietario de un vehículo.
        /// </summary>
        /// <param name="vehicleId">Identificador del vehículo a actualizar.</param>
        /// <param name="newOwner">Nuevo propietario (<see cref="Owner"/>) para el vehículo.</param>
        Task ChangeVehicleOwnerAsync(int vehicleId, Owner newOwner);

        /// <summary>
        /// Agrega accesorios a un vehículo.
        /// </summary>
        /// <param name="vehicleId">Identificador del vehículo al que se agregarán los accesorios.</param>
        /// <param name="accessories">Lista de identificadores de accesorios a agregar.</param>
        Task AddVehicleAccessories(int vehicleId, List<int> accessories);

        /// <summary>
        /// Actualiza en bloque un vehículo específico, modificando varias propiedades a la vez mediante <c>ExecuteUpdateAsync</c>.
        /// </summary>
        /// <param name="vehicleId">Identificador del vehículo a actualizar.</param>
        /// <param name="newLicensePlate">Nuevo número de placa.</param>
        /// <param name="fuelType">Nuevo tipo de combustible.</param>
        /// <param name="engineDisplacement">Nueva cilindrada del motor.</param>
        /// <param name="horsepower">Nueva potencia del motor.</param>
        Task ExecuteVehicleUpdateAsync(
            int vehicleId,
            string newLicensePlate,
            string fuelType,
            int engineDisplacement,
            int horsepower);

        /// <summary>
        /// Elimina un vehículo por su identificador.
        /// </summary>
        /// <param name="vehicleId">Identificador del vehículo a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación, si es necesario.</param>
        Task DeleteVehicleByIdAsync(int vehicleId);

        /// <summary>
        /// Actualiza parcialmente un vehículo, marcando únicamente las propiedades que tienen un valor nuevo.
        /// La entidad se adjunta al <see cref="DbContext"/> y se marcan las propiedades modificadas.
        /// La persistencia se realiza de forma centralizada mediante Unit of Work o pipeline.
        /// </summary>
        /// <param name="vehicle">
        /// La entidad <see cref="Vehicle"/> que contiene los nuevos valores. Se actualizan únicamente las propiedades no nulas o con valores distintos de cero.
        /// </param>
        Task PartialVehicleUpdateAsync(Vehicle vehicle);

        /// <summary>
        /// Actualiza de forma parcial un conjunto de vehículos, marcando únicamente las propiedades que tengan un valor nuevo en cada entidad.
        /// Cada vehículo se adjunta al <see cref="DbContext"/> y se marcan las propiedades modificadas.
        /// La persistencia se ejecuta de forma centralizada.
        /// </summary>
        /// <param name="vehicles">Colección de vehículos a actualizar parcialmente.</param>
        Task PartialVehiclesBulkUpdateAsync(IEnumerable<Vehicle> vehicles);

        /// <summary>
        /// Obtiene la lista de vehículos de forma paginada, ordenada y filtrada.
        /// </summary>
        /// <param name="sortCriteria"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(List<Vehicle> Vehicles, int TotalCount)> VehicleDynamicSort(List<SortCriteriaDto> sortCriteria, int pageNumber, int pageSize);
    }
}
