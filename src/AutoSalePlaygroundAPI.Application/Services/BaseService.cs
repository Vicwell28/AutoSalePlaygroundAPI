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
    /// <typeparam name="T">
    /// Tipo de entidad que implementa la interfaz <see cref="IEntity"/>.
    /// </typeparam>
    /// <remarks>
    /// Inicializa una nueva instancia de la clase <see cref="BaseService{T}"/>.
    /// </remarks>
    /// <param name="repository">Repositorio genérico para la entidad.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el repositorio es <c>null</c>.</exception>
    public abstract class BaseService<T>(IRepository<T> repository) : IBaseService<T> where T : class, IEntity
    {
        /// <summary>
        /// Repositorio genérico utilizado para todas las operaciones de lectura y escritura.
        /// </summary>
        protected readonly IRepository<T> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        /// <inheritdoc />
        public async virtual Task<IEnumerable<T>> ToListAsync(bool trackChanges = false, CancellationToken cancellationToken = default) =>
            await _repository.ToListAsync(trackChanges, cancellationToken);

        /// <inheritdoc />
        public async virtual Task AddAsync(T entity, CancellationToken cancellationToken = default) =>
            await _repository.AddAsync(entity, cancellationToken);

        /// <inheritdoc />
        public async virtual Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) =>
            await _repository.AddRangeAsync(entities, cancellationToken);

        /// <inheritdoc />
        public async virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default) =>
            await _repository.UpdateAsync(entity, cancellationToken);

        /// <inheritdoc />
        public async virtual Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) =>
            await _repository.UpdateRangeAsync(entities, cancellationToken);

        /// <inheritdoc />
        public async virtual Task RemoveAsync(T entity, CancellationToken cancellationToken = default) =>
            await _repository.RemoveAsync(entity, cancellationToken);

        /// <inheritdoc />
        public async virtual Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) =>
            await _repository.RemoveRangeAsync(entities, cancellationToken);

        /// <inheritdoc />
        public async virtual Task<List<T>> ListAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default) =>
                await _repository.ListAsync(specification, trackChanges, cancellationToken);

        /// <inheritdoc />
        public async virtual Task<T?> FirstOrDefaultAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default) =>
                await _repository.FirstOrDefaultAsync(specification, trackChanges, cancellationToken);

        /// <inheritdoc />
        public async virtual Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default) =>
                await _repository.ListAsync(specification, selector, trackChanges, cancellationToken);

        /// <inheritdoc />
        public async virtual Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default) =>
                await _repository.FirstOrDefaultAsync(specification, selector, trackChanges, cancellationToken);

        /// <inheritdoc />
        public async virtual Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default) =>
                await _repository.ListPaginatedAsync(specification, trackChanges, cancellationToken);
    }
}
