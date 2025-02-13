using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Contrato base para servicios de aplicación que operan sobre entidades del dominio.
    /// Define métodos comunes para operaciones CRUD y consultas basadas en especificaciones,
    /// permitiendo configurar el tracking de entidades y la cancelación de operaciones asíncronas.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad que implementa la interfaz <see cref="IEntity"/>.</typeparam>
    public interface IBaseService<T> where T : class, IEntity
    {
        /// <summary>
        /// Obtiene todas las entidades de forma asíncrona.
        /// </summary>
        /// <param name="trackChanges">Indica si se debe habilitar el tracking de las entidades.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una colección de entidades.</returns>
        Task<IEnumerable<T>> ToListAsync(bool trackChanges = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega una nueva entidad de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega un rango de entidades de forma asíncrona.
        /// </summary>
        /// <param name="entities">La colección de entidades a agregar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza una entidad existente de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza un rango de entidades existentes de forma asíncrona.
        /// </summary>
        /// <param name="entities">La colección de entidades a actualizar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina una entidad de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina un rango de entidades de forma asíncrona.
        /// </summary>
        /// <param name="entities">La colección de entidades a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una lista de entidades que cumplen con la especificación dada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="trackChanges">Indica si se debe habilitar el tracking de las entidades.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una lista de entidades que cumplen la especificación.</returns>
        Task<List<T>> ListAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la primera entidad que cumple con la especificación dada o <c>null</c> si no se encuentra.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="trackChanges">Indica si se debe habilitar el tracking de la entidad.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>La primera entidad que cumple con la especificación o <c>null</c>.</returns>
        Task<T?> FirstOrDefaultAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una lista de resultados proyectados según la especificación y el selector.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado proyectado.</typeparam>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="selector">La expresión que define la proyección de la entidad.</param>
        /// <param name="trackChanges">Indica si se debe habilitar el tracking de las entidades.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>Una lista de resultados proyectados.</returns>
        Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el primer resultado proyectado según la especificación y el selector, o <c>null</c> si no se encuentra.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado proyectado.</typeparam>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="selector">La expresión que define la proyección de la entidad.</param>
        /// <param name="trackChanges">Indica si se debe habilitar el tracking de la entidad.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>El primer resultado proyectado o <c>null</c>.</returns>
        Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una lista paginada de entidades y el total de registros que cumplen con la especificación.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta y paginación.</param>
        /// <param name="trackChanges">Indica si se debe habilitar el tracking de las entidades.</param>
        /// <param name="cancellationToken">Token para cancelar la operación.</param>
        /// <returns>
        /// Una tupla que contiene:
        /// <list type="bullet">
        ///   <item><c>Data</c>: la lista de entidades que cumplen con la especificación.</item>
        ///   <item><c>TotalCount</c>: el total de registros sin aplicar paginación.</item>
        /// </list>
        /// </returns>
        Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);
    }
}
