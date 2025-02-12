using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Interfaces
{
    /// <summary>
    /// Contrato para operaciones de escritura sobre entidades.
    /// Proporciona métodos para inserción, actualización, borrado y operaciones avanzadas de escritura.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public interface IWriteRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Obtiene el contexto de base de datos actual.
        /// </summary>
        AutoSalePlaygroundAPIDbContext DbContext { get; }

        #region Métodos de Consulta mediante ISpecification

        /// <summary>
        /// Obtiene una lista de entidades que cumplen con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <returns>Una lista de entidades que cumplen con la especificación.</returns>
        Task<List<T>> ListAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene la primera entidad que cumple con la especificación indicada o <c>null</c> si no existe ninguna.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <returns>La primera entidad que cumple con la especificación o <c>null</c>.</returns>
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene una lista de resultados proyectados mediante el selector indicado a partir de la especificación.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado proyectado.</typeparam>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="selector">La expresión que define la proyección.</param>
        /// <returns>Una lista de resultados proyectados.</returns>
        Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene el primer resultado proyectado mediante el selector indicado a partir de la especificación.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado proyectado.</typeparam>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="selector">La expresión que define la proyección.</param>
        /// <returns>El primer resultado proyectado o <c>null</c> si no existe ninguno.</returns>
        Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene una lista paginada de entidades junto con el total de registros que cumplen con la especificación.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta y paginación.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>
        /// Una tupla que contiene:
        /// <list type="bullet">
        ///   <item><c>Data</c>: la lista de entidades.</item>
        ///   <item><c>TotalCount</c>: el total de registros sin paginación.</item>
        /// </list>
        /// </returns>
        Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(
            ISpecification<T> specification,
            CancellationToken cancellationToken = default);

        #endregion

        #region Métodos CRUD

        /// <summary>
        /// Agrega una nueva entidad al conjunto de datos de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Agrega un rango de entidades al conjunto de datos de forma asíncrona.
        /// </summary>
        /// <param name="entities">La colección de entidades a agregar.</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza una entidad existente en el conjunto de datos.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Actualiza un rango de entidades existentes en el conjunto de datos.
        /// </summary>
        /// <param name="entities">La colección de entidades a actualizar.</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Elimina una entidad del conjunto de datos.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Elimina un rango de entidades del conjunto de datos.
        /// </summary>
        /// <param name="entities">La colección de entidades a eliminar.</param>
        Task RemoveRangeAsync(IEnumerable<T> entities);

        #endregion

        #region Métodos de Escritura Avanzados

        /// <summary>
        /// Ejecuta una operación de borrado masivo para las entidades que cumplan con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define las entidades a eliminar.</param>
        Task ExecuteDeleteAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Ejecuta una operación de actualización masiva para las entidades que cumplan con el predicado indicado,
        /// aplicando la actualización definida en la expresión.
        /// </summary>
        /// <param name="predicate">La expresión que define las entidades a actualizar.</param>
        /// <param name="updateExpression">
        /// La expresión que define las propiedades a actualizar y sus nuevos valores.
        /// Se utiliza la clase <see cref="SetPropertyCalls{T}"/> para construir la actualización.
        /// </param>
        Task ExecuteUpdateAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);

        #endregion
    }
}
