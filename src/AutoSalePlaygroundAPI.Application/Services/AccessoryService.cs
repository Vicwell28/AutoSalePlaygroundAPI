using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con accesorios.
    /// Extiende las operaciones genéricas definidas en <see cref="BaseService{Accessory}"/>
    /// e implementa métodos específicos para la entidad <see cref="Accessory"/>.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia de <see cref="AccessoryService"/>.
    /// </remarks>
    /// <param name="accessoryRepository">Repositorio para la entidad <see cref="Accessory"/>.</param>
    /// <exception cref="ArgumentNullException">Se lanza si <paramref name="accessoryRepository"/> es null.</exception>
    public class AccessoryService(IRepository<Accessory> accessoryRepository)
        : BaseService<Accessory>(accessoryRepository), IAccessoryService
    {
        /// <inheritdoc />
        public async Task<Accessory?> GetAccessoryByIdAsync(int accessoryId)
        {
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            return await base.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<Accessory>> GetAllActiveAccessoriesAsync()
        {
            var spec = new ActiveAccessorySpec();
            return await base.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task AddNewAccessoryAsync(string name)
        {
            var accessory = new Accessory(name);
            await base.AddAsync(accessory);
        }

        /// <inheritdoc />
        public async Task UpdateAccessoryNameAsync(int accessoryId, string newName)
        {
            var accessory = await GetAccessoryByIdAsync(accessoryId);

            if (accessory == null)
            {
                throw new NotFoundException("Accesorio no encontrado.");
            }

            accessory.UpdateName(newName);
            
            await base.UpdateAsync(accessory);
        }
    }
}
