using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones relacionadas con accesorios.
    /// </summary>
    public class AccessoryService : IAccessoryService
    {
        private readonly IRepository<Accessory> _accessoryRepository;

        public AccessoryService(IRepository<Accessory> accessoryRepository)
        {
            _accessoryRepository = accessoryRepository;
        }

        /// <summary>
        /// Obtiene un accesorio por su Id.
        /// </summary>
        public async Task<Accessory?> GetAccessoryByIdAsync(int accessoryId)
        {
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            return await _accessoryRepository.FirstOrDefaultAsync(spec);
        }

        /// <summary>
        /// Obtiene todos los accesorios activos.
        /// </summary>
        public async Task<List<Accessory>> GetAllActiveAccessoriesAsync()
        {
            var spec = new ActiveAccessorySpec();
            return await _accessoryRepository.ListAsync(spec);
        }

        /// <summary>
        /// Agrega un nuevo accesorio.
        /// </summary>
        public async Task AddNewAccessoryAsync(string name)
        {
            var accessory = new Accessory(name);
            await _accessoryRepository.AddAsync(accessory);
        }

        /// <summary>
        /// Actualiza el nombre de un accesorio.
        /// </summary>
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
