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
    /// Inicializa una nueva instancia de <see cref="AccessoryService"/> inyectando un repositorio de escritura
    /// y uno de lectura para la entidad <see cref="Accessory"/>.
    /// Se lanzará una excepción <see cref="ArgumentNullException"/> si alguno de los repositorios es <c>null</c>.
    /// </remarks>
    public class AccessoryService(
        IWriteRepository<Accessory> accessoryWriteRepository,
        IReadRepository<Accessory> accessoryReadRepository)
        : BaseService<Accessory>(accessoryWriteRepository, accessoryReadRepository), IAccessoryService
    {
        /// <inheritdoc />
        public async Task<Accessory?> GetAccessoryByIdAsync(int accessoryId)
        {
            // Se utiliza una especificación genérica para filtrar por identificador.
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            return await base.FirstOrDefaultAsync(spec);
        }

        /// <inheritdoc />
        public async Task<List<Accessory>> GetAllActiveAccessoriesAsync()
        {
            // Se utiliza una especificación que filtra únicamente accesorios activos.
            var spec = new ActiveAccessorySpec();
            return await base.ListAsync(spec);
        }

        /// <inheritdoc />
        public async Task AddNewAccessoryAsync(string name)
        {
            // Se crea una nueva instancia de Accessory y se agrega al repositorio.
            var accessory = new Accessory(name);
            await base.AddAsync(accessory);
        }

        /// <inheritdoc />
        public async Task UpdateAccessoryNameAsync(int accessoryId, string newName)
        {
            // Se obtiene el accesorio por su identificador.
            var spec = new GenericAccessorySpec(a => a.Id == accessoryId);
            var accessory = await _writeRepository.FirstOrDefaultAsync(spec);

            if (accessory == null)
            {
                // Si no se encuentra el accesorio, se lanza una excepción personalizada.
                throw new NotFoundException("Accesorio no encontrado.");
            }

            // Se actualiza el nombre y se marca la entidad como actualizada.
            accessory.UpdateName(newName);

            await base.UpdateAsync(accessory);
        }
    }
}
