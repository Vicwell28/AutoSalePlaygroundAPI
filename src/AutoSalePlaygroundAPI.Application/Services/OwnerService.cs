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
    /// e implementa métodos específicos para la entidad <see cref="Owner"/>.
    /// </summary>
    /// <remarks>
    /// Se requiere inyectar un repositorio de escritura y uno de lectura para la entidad <see cref="Owner"/>.
    /// Se lanzará una excepción <see cref="ArgumentNullException"/> si alguno de ellos es <c>null</c>.
    /// </remarks>
    public class OwnerService : BaseService<Owner>, IOwnerService
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="OwnerService"/>.
        /// </summary>
        /// <param name="ownerWriteRepository">
        /// Repositorio de escritura para la entidad <see cref="Owner"/>.
        /// </param>
        /// <param name="ownerReadRepository">
        /// Repositorio de lectura para la entidad <see cref="Owner"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si alguno de los repositorios es <c>null</c>.
        /// </exception>
        public OwnerService(IWriteRepository<Owner> ownerWriteRepository, IReadRepository<Owner> ownerReadRepository)
            : base(ownerWriteRepository, ownerReadRepository)
        {
        }

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
            var spec = new OwnerActiveSpec(ownerId);
            var owner = await _writeRepository.FirstOrDefaultAsync(spec);

            if (owner == null)
            {
                throw new NotFoundException("Propietario no encontrado.");
            }

            owner.UpdateName(newFirstName, newLastName);

            await base.UpdateAsync(owner);
        }
    }
}
