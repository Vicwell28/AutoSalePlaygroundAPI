using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con propietarios.
    /// Extiende las operaciones genéricas definidas en <see cref="BaseService{Owner}"/>
    /// y agrega métodos específicos para la entidad <see cref="Owner"/>.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia de <see cref="OwnerService"/>.
    /// </remarks>
    /// <param name="ownerRepository">Repositorio para la entidad <see cref="Owner"/>.</param>
    /// <exception cref="ArgumentNullException">Se lanza si <paramref name="ownerRepository"/> es null.</exception>
    public class OwnerService(IRepository<Owner> ownerRepository)
        : BaseService<Owner>(ownerRepository), IOwnerService
    {
        /// <inheritdoc />
        public async Task<Owner?> GetOwnerByIdAsync(int ownerId)
        {
            var spec = new OwnerActiveSpec(ownerId);
            return await base.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<Owner>> GetAllActiveOwnersAsync()
        {
            var spec = new ActiveOwnerSpec();
            return await base.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task AddNewOwnerAsync(string firstName, string lastName)
        {
            var owner = new Owner(firstName, lastName);
            await base.AddAsync(owner);
        }

        /// <inheritdoc />
        public async Task UpdateOwnerNameAsync(int ownerId, string newFirstName, string newLastName)
        {
            var owner = await GetOwnerByIdAsync(ownerId);

            if (owner == null)
            {
                throw new NotFoundException("Propietario no encontrado.");
            }

            owner.UpdateName(newFirstName, newLastName);

            await base.UpdateAsync(owner);
        }
    }
}
