using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    /// <summary>
    /// Interfaz base para servicios que operan sobre entidades del dominio.
    /// Proporciona métodos CRUD y consultas basadas en especificaciones.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad que implementa la interfaz <see cref="IEntity"/>.
    /// </typeparam>
    public interface IBaseService<T> where T : class, IEntity
    {
        /// <summary>
        /// Obtiene una lista completa de todas las entidades.
        /// </summary>
        /// <returns>
        /// Una colección de entidades de tipo <typeparamref name="T"/>.
        /// </returns>
        Task<IEnumerable<T>> ToListAsync();

        /// <summary>
        /// Agrega una nueva entidad.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Agrega un rango de entidades.
        /// </summary>
        /// <param name="entities">Colección de entidades a agregar.</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Actualiza un rango de entidades.
        /// </summary>
        /// <param name="entities">Colección de entidades a actualizar.</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Elimina una entidad.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Elimina un rango de entidades.
        /// </summary>
        /// <param name="entities">Colección de entidades a eliminar.</param>
        Task RemoveRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Lista las entidades que cumplen con una especificación dada.
        /// </summary>
        /// <param name="specification">
        /// La especificación que define el filtro.
        /// </param>
        /// <returns>
        /// Lista de entidades que cumplen la especificación.
        /// </returns>
        Task<List<T>> ListAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene la primera entidad que cumple con una especificación.
        /// </summary>
        /// <param name="specification">
        /// La especificación a evaluar.
        /// </param>
        /// <returns>
        /// La primera entidad que cumple la especificación o <c>null</c> si no se encuentra.
        /// </returns>
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);

        /// <summary>
        /// Lista las entidades que cumplen con una especificación y las proyecta a un tipo específico.
        /// </summary>
        /// <typeparam name="TResult">
        /// Tipo de resultado de la proyección.
        /// </typeparam>
        /// <param name="specification">
        /// La especificación que define el filtro.
        /// </param>
        /// <param name="selector">
        /// Expresión que define la proyección.
        /// </param>
        /// <returns>
        /// Lista de resultados proyectados.
        /// </returns>
        Task<List<TResult>> ListAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene la primera entidad que cumple con una especificación, proyectada a un tipo específico.
        /// </summary>
        /// <typeparam name="TResult">
        /// Tipo de resultado de la proyección.
        /// </typeparam>
        /// <param name="specification">
        /// La especificación a evaluar.
        /// </param>
        /// <param name="selector">
        /// Expresión que define la proyección.
        /// </param>
        /// <returns>
        /// El primer resultado proyectado o <c>null</c> si no se encuentra.
        /// </returns>
        Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Lista las entidades de forma paginada basadas en una especificación.
        /// </summary>
        /// <param name="specification">
        /// La especificación que define el filtro y la paginación.
        /// </param>
        /// <param name="cancellationToken">
        /// Token para cancelar la operación, si es necesario.
        /// </param>
        /// <returns>
        /// Una tupla que contiene la lista de entidades que cumplen la especificación y el total de registros.
        /// </returns>
        Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    }
}
