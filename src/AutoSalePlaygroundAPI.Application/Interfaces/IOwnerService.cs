using AutoSalePlaygroundAPI.Domain.Entities;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato del servicio de aplicación para gestionar operaciones relacionadas con propietarios.
    /// Extiende los métodos genéricos definidos en <see cref="IBaseService{Owner}"/>.
    /// </summary>
    public interface IOwnerService : IBaseService<Owner>
    {
        /// <summary>
        /// Obtiene un propietario por su identificador.
        /// </summary>
        /// <param name="ownerId">El identificador del propietario.</param>
        /// <returns>
        /// Una tarea asíncrona cuyo resultado es el propietario encontrado o <c>null</c> si no se encuentra.
        /// </returns>
        Task<Owner?> GetOwnerByIdAsync(int ownerId);

        /// <summary>
        /// Obtiene todos los propietarios activos.
        /// </summary>
        /// <returns>
        /// Una tarea asíncrona cuyo resultado es una lista de propietarios activos.
        /// </returns>
        Task<List<Owner>> GetAllActiveOwnersAsync();

        /// <summary>
        /// Agrega un nuevo propietario utilizando el nombre y apellido especificados.
        /// </summary>
        /// <param name="firstName">El nombre del propietario.</param>
        /// <param name="lastName">El apellido del propietario.</param>
        /// <returns>
        /// Una tarea asíncrona que representa la operación.
        /// </returns>
        Task AddNewOwnerAsync(string firstName, string lastName);

        /// <summary>
        /// Actualiza el nombre y apellido de un propietario.
        /// </summary>
        /// <param name="ownerId">El identificador del propietario a actualizar.</param>
        /// <param name="newFirstName">El nuevo nombre.</param>
        /// <param name="newLastName">El nuevo apellido.</param>
        /// <returns>
        /// Una tarea asíncrona que representa la operación.
        /// </returns>
        Task UpdateOwnerNameAsync(int ownerId, string newFirstName, string newLastName);
    }
}
