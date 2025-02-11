using AutoSalePlaygroundAPI.Domain.Entities;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato del servicio de aplicación para gestionar operaciones relacionadas con propietarios.
    /// </summary>
    public interface IOwnerService : IBaseService<Owner>
    {
        /// <summary>
        /// Obtiene un propietario por su Id.
        /// </summary>
        Task<Owner?> GetOwnerByIdAsync(int ownerId);

        /// <summary>
        /// Obtiene todos los propietarios activos.
        /// </summary>
        Task<List<Owner>> GetAllActiveOwnersAsync();

        /// <summary>
        /// Agrega un nuevo propietario.
        /// </summary>
        Task AddNewOwnerAsync(string firstName, string lastName);

        /// <summary>
        /// Actualiza el nombre de un propietario.
        /// </summary>
        Task UpdateOwnerNameAsync(int ownerId, string newFirstName, string newLastName);
    }
}