using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con propietarios.
    /// </summary>
    public class OwnerService(IRepository<Owner> ownerRepository) : IOwnerService
    {
        private readonly IRepository<Owner> _ownerRepository = ownerRepository
            ?? throw new ArgumentNullException(nameof(ownerRepository));

        /// <inheritdoc />
        public async Task<Owner?> GetOwnerByIdAsync(int ownerId)
        {
            // Se asume que existe una especificación genérica para propietarios.
            var spec = new OwnerActiveSpec(ownerId);
            //var spec = new GenericOwnerSpec(o => o.Id == ownerId);
            return await _ownerRepository.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<Owner>> GetAllActiveOwnersAsync()
        {
            var spec = new ActiveOwnerSpec(); // Esta especificación filtra por IsActive.
            return await _ownerRepository.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task AddNewOwnerAsync(string firstName, string lastName)
        {
            var owner = new Owner(firstName, lastName);
            await _ownerRepository.AddAsync(owner);
        }

        /// <inheritdoc />
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
