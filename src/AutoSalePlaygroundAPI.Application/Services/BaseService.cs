using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Implementación base para servicios que operan sobre entidades del dominio.
    /// Utiliza un repositorio genérico para delegar las operaciones CRUD y las consultas basadas en especificaciones.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad que implementa la interfaz <see cref="IEntity"/>.
    /// </typeparam>
    /// <remarks>
    /// La clase requiere inyectar un repositorio de escritura (<see cref="IWriteRepository{T}"/>) y uno de lectura (<see cref="IReadRepository{T}"/>).
    /// Se lanzará una excepción <see cref="ArgumentNullException"/> si alguno de ellos es <c>null</c>.
    /// </remarks>
    public abstract class BaseService<T>(IWriteRepository<T> writeRepository, IReadRepository<T> readRepository)
        : IBaseService<T> where T : class, IEntity
    {
        /// <inheritdoc />
        protected readonly IWriteRepository<T> _writeRepository = writeRepository
            ?? throw new ArgumentNullException(nameof(writeRepository));

        /// <inheritdoc />
        protected readonly IReadRepository<T> _readRepository = readRepository
            ?? throw new ArgumentNullException(nameof(readRepository));

        /// <inheritdoc />
        public async virtual Task<IEnumerable<T>> ToListAsync()
        {
            // Obtiene todas las entidades utilizando el repositorio de lectura.
            return await _readRepository.ToListAsync();
        }

        /// <inheritdoc />
        public async virtual Task AddAsync(T entity)
        {
            // Agrega una nueva entidad utilizando el repositorio de escritura.
            await _writeRepository.AddAsync(entity);
        }

        /// <inheritdoc />
        public async virtual Task AddRangeAsync(IEnumerable<T> entities)
        {
            // Agrega un conjunto de entidades utilizando el repositorio de escritura.
            await _writeRepository.AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public async virtual Task UpdateAsync(T entity)
        {
            // Actualiza la entidad utilizando el repositorio de escritura.
            await _writeRepository.UpdateAsync(entity);
        }

        /// <inheritdoc />
        public async virtual Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            // Actualiza un conjunto de entidades utilizando el repositorio de escritura.
            await _writeRepository.UpdateRangeAsync(entities);
        }

        /// <inheritdoc />
        public async virtual Task RemoveAsync(T entity)
        {
            // Elimina la entidad utilizando el repositorio de escritura.
            await _writeRepository.RemoveAsync(entity);
        }

        /// <inheritdoc />
        public async virtual Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            // Elimina un conjunto de entidades utilizando el repositorio de escritura.
            await _writeRepository.RemoveRangeAsync(entities);
        }

        /// <inheritdoc />
        public async virtual Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            // Lista las entidades que cumplen con la especificación utilizando el repositorio de lectura.
            return await _readRepository.ListAsync(specification);
        }

        /// <inheritdoc />
        public async virtual Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            // Obtiene la primera entidad que cumple la especificación utilizando el repositorio de lectura.
            return await _readRepository.FirstOrDefaultAsync(specification);
        }

        /// <inheritdoc />
        public async virtual Task<List<TResult>> ListAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector)
        {
            // Lista y proyecta las entidades que cumplen con la especificación según el selector utilizando el repositorio de lectura.
            return await _readRepository.ListAsync(specification, selector);
        }

        /// <inheritdoc />
        public async virtual Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector)
        {
            // Obtiene el primer resultado proyectado según la especificación utilizando el repositorio de lectura.
            return await _readRepository.FirstOrDefaultAsync(specification, selector);
        }

        /// <inheritdoc />
        public async virtual Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            // Retorna una lista paginada de entidades y el total de registros utilizando el repositorio de lectura.
            return await _readRepository.ListPaginatedAsync(specification, cancellationToken);
        }
    }
}
