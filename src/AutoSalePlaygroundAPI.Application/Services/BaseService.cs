using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Application.Services
{
    /// <summary>
    /// Implementación base para servicios que operan sobre entidades del dominio.
    /// Utiliza un repositorio genérico para delegar las operaciones CRUD y las consultas basadas en especificaciones.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que implementa la interfaz <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// Inicializa una nueva instancia de la clase <see cref="BaseService{T}"/>.
    /// </remarks>
    /// <param name="repository">Repositorio para la entidad <typeparamref name="T"/>.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el repositorio es <c>null</c>.</exception>
    public abstract class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class, IEntity
    {
        /// <summary>
        /// Repositorio genérico que provee las operaciones CRUD para la entidad.
        /// </summary>
        protected readonly IRepository<T> _repository = repository
            ?? throw new ArgumentNullException(nameof(repository));

        /// <inheritdoc />
        public async virtual Task<IEnumerable<T>> ToListAsync()
        {
            // Obtiene todas las entidades utilizando el repositorio.
            return await _repository.ToListAsync();
        }

        /// <inheritdoc />
        public async virtual Task AddAsync(T entity)
        {
            // Agrega una nueva entidad utilizando el repositorio.
            await _repository.AddAsync(entity);
        }

        /// <inheritdoc />
        public async virtual Task AddRangeAsync(IEnumerable<T> entities)
        {
            // Agrega un conjunto de entidades utilizando el repositorio.
            await _repository.AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public async virtual Task UpdateAsync(T entity)
        {
            // Actualiza la entidad utilizando el repositorio.
            await _repository.UpdateAsync(entity);
        }

        /// <inheritdoc />
        public async virtual Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            // Actualiza un conjunto de entidades utilizando el repositorio.
            await _repository.UpdateRangeAsync(entities);
        }

        /// <inheritdoc />
        public async virtual Task RemoveAsync(T entity)
        {
            // Elimina la entidad utilizando el repositorio.
            await _repository.RemoveAsync(entity);
        }

        /// <inheritdoc />
        public async virtual Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            // Elimina un conjunto de entidades utilizando el repositorio.
            await _repository.RemoveRangeAsync(entities);
        }

        /// <inheritdoc />
        public async virtual Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            // Lista las entidades que cumplen con la especificación.
            return await _repository.ListAsync(specification);
        }

        /// <inheritdoc />
        public async virtual Task<T?> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            // Obtiene la primera entidad que cumple la especificación o null si no existe.
            return await _repository.FirstOrDefaultAsync(specification);
        }

        /// <inheritdoc />
        public async virtual Task<List<TResult>> ListAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector)
        {
            // Lista y proyecta las entidades que cumplen con la especificación según el selector.
            return await _repository.ListAsync(specification, selector);
        }

        /// <inheritdoc />
        public async virtual Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector)
        {
            // Obtiene el primer resultado proyectado según la especificación o null si no existe.
            return await _repository.FirstOrDefaultAsync(specification, selector);
        }

        /// <inheritdoc />
        public async virtual Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            // Retorna una lista paginada de entidades junto con el total de registros que cumplen la especificación.
            return await _repository.ListPaginatedAsync(specification, cancellationToken);
        }
    }
}
