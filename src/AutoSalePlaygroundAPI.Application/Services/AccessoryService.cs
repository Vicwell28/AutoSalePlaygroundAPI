using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con accesorios.
    /// </summary>
    public class AccessoryService(IRepository<Accessory> accessoryRepository) : IAccessoryService
    {
        private readonly IRepository<Accessory> _accessoryRepository = accessoryRepository
            ?? throw new ArgumentNullException(nameof(accessoryRepository));

        /// <inheritdoc />
        public async Task<Accessory?> GetAccessoryByIdAsync(int accessoryId)
        {
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            return await _accessoryRepository.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<Accessory>> GetAllActiveAccessoriesAsync()
        {
            var spec = new ActiveAccessorySpec();
            return await _accessoryRepository.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task AddNewAccessoryAsync(string name)
        {
            var accessory = new Accessory(name);
            await _accessoryRepository.AddAsync(accessory);
        }

        /// <inheritdoc />
        public async Task UpdateAccessoryNameAsync(int accessoryId, string newName)
        {
            var accessory = await GetAccessoryByIdAsync(accessoryId);
            if (accessory == null)
            {
                throw new Exception("Accesorio no encontrado.");
            }
            accessory.UpdateName(newName);
            await _accessoryRepository.UpdateAsync(accessory);
        }
    }
}
