using AutoSalePlaygroundAPI.Domain.Entities;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato del servicio de aplicación para gestionar operaciones relacionadas con accesorios.
    /// Extiende los métodos genéricos definidos en <see cref="IBaseService{Accessory}"/>.
    /// </summary>
    public interface IAccessoryService : IBaseService<Accessory>
    {
        /// <summary>
        /// Obtiene un accesorio por su identificador.
        /// </summary>
        /// <param name="accessoryId">El identificador del accesorio.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. El resultado contiene el accesorio encontrado,
        /// o <c>null</c> si no se encuentra.
        /// </returns>
        Task<Accessory?> GetAccessoryByIdAsync(int accessoryId);

        /// <summary>
        /// Obtiene todos los accesorios activos.
        /// </summary>
        /// <returns>
        /// Una tarea que representa la operación asíncrona. El resultado es una lista de accesorios activos.
        /// </returns>
        Task<List<Accessory>> GetAllActiveAccessoriesAsync();

        /// <summary>
        /// Agrega un nuevo accesorio utilizando el nombre proporcionado.
        /// </summary>
        /// <param name="name">El nombre del nuevo accesorio.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// </returns>
        Task AddNewAccessoryAsync(string name);

        /// <summary>
        /// Actualiza el nombre de un accesorio.
        /// </summary>
        /// <param name="accessoryId">El identificador del accesorio a actualizar.</param>
        /// <param name="newName">El nuevo nombre que se asignará al accesorio.</param>
        /// <returns>
        /// Una tarea que representa la operación asíncrona.
        /// </returns>
        Task UpdateAccessoryNameAsync(int accessoryId, string newName);
    }
}
