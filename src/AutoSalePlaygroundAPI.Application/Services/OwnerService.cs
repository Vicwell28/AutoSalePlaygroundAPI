using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con propietarios.
    /// </summary>
    public class OwnerService : IOwnerService
    {
        private readonly IRepository<Owner> _ownerRepository;

        public OwnerService(IRepository<Owner> ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        /// <summary>
        /// Obtiene un propietario por su Id.
        /// </summary>
        public async Task<Owner?> GetOwnerByIdAsync(int ownerId)
        {
            // Se asume que existe una especificación genérica para propietarios.
            var spec = new GenericOwnerSpec(o => o.Id == ownerId);
            return await _ownerRepository.FirstOrDefaultAsync(spec);
        }

        /// <summary>
        /// Obtiene todos los propietarios activos.
        /// </summary>
        public async Task<List<Owner>> GetAllActiveOwnersAsync()
        {
            var spec = new ActiveOwnerSpec(); // Esta especificación filtra por IsActive.
            return await _ownerRepository.ListAsync(spec);
        }

        /// <summary>
        /// Agrega un nuevo propietario.
        /// </summary>
        public async Task AddNewOwnerAsync(string firstName, string lastName)
        {
            var owner = new Owner(firstName, lastName);
            await _ownerRepository.AddAsync(owner);
        }

        /// <summary>
        /// Actualiza el nombre de un propietario.
        /// </summary>
        public async Task UpdateOwnerNameAsync(int ownerId, string newFirstName, string newLastName)
        {
            var owner = await GetOwnerByIdAsync(ownerId);
            if (owner == null)
            {
                throw new Exception("Propietario no encontrado.");
            }
            owner.UpdateName(newFirstName, newLastName);
            await _ownerRepository.UpdateAsync(owner);
        }
    }
}
