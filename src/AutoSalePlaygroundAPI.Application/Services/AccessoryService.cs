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
    /// <param name="accessoryRepository">Repositorio genérico para la entidad <see cref="Accessory"/>.</param>
    public class AccessoryService(IRepository<Accessory> accessoryRepository) : BaseService<Accessory>(accessoryRepository), IAccessoryService
    {
        /// <inheritdoc />
        public async Task<Accessory?> GetAccessoryByIdAsync(int accessoryId)
        {
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            return await FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<Accessory>> GetAllActiveAccessoriesAsync()
        {
            var spec = new ActiveAccessorySpec();
            return await ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task AddNewAccessoryAsync(string name)
        {
            var accessory = new Accessory(name);
            await AddAsync(accessory);
        }

        /// <inheritdoc />
        public async Task UpdateAccessoryNameAsync(int accessoryId, string newName)
        {
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            var accessory = await _repository.FirstOrDefaultAsync(spec);

            if (accessory == null)
            {
                throw new NotFoundException("Accesorio no encontrado.");
            }

            accessory.UpdateName(newName);
            await UpdateAsync(accessory);
        }
    }
}
