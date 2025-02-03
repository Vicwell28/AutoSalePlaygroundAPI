using AutoSalePlaygroundAPI.Domain.Entities;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato del servicio de aplicación para gestionar operaciones relacionadas con accesorios.
    /// </summary>
    public interface IAccessoryService
    {
        /// <summary>
        /// Obtiene un accesorio por su Id.
        /// </summary>
        Task<Accessory?> GetAccessoryByIdAsync(int accessoryId);

        /// <summary>
        /// Obtiene todos los accesorios activos.
        /// </summary>
        Task<List<Accessory>> GetAllActiveAccessoriesAsync();

        /// <summary>
        /// Agrega un nuevo accesorio.
        /// </summary>
        Task AddNewAccessoryAsync(string name);

        /// <summary>
        /// Actualiza el nombre de un accesorio.
        /// </summary>
        Task UpdateAccessoryNameAsync(int accessoryId, string newName);
    }
}