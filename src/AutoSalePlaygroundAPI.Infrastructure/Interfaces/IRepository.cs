using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Interfaces
{
    /// <summary>
    /// Define el contrato para el repositorio genérico que gestiona operaciones de acceso a datos.
    /// Permite realizar operaciones CRUD, consultas mediante especificaciones, agregaciones y operaciones en masa.
    /// Se incorpora un parámetro opcional para habilitar o no el tracking de entidades.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que implementa <see cref="IEntity"/>.</typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Obtiene el contexto de base de datos subyacente.
        /// </summary>
        AutoSalePlaygroundAPIDbContext DbContext { get; }

        #region Métodos de Consulta mediante ISpecification (con opción de tracking)

        /// <summary>
        /// Obtiene una lista de entidades que cumplen con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>Una lista de entidades que cumplen con la especificación.</returns>
        Task<List<T>> ListAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la primera entidad que cumple con la especificación indicada o <c>null</c> si no existe ninguna.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La primera entidad que cumple con la especificación o <c>null</c>.</returns>
        Task<T?> FirstOrDefaultAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una lista de resultados proyectados a partir de la especificación y el selector indicado.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado proyectado.</typeparam>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="selector">La expresión que define la proyección de la entidad.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>Una lista de resultados proyectados.</returns>
        Task<List<TResult>> ListAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el primer resultado proyectado a partir de la especificación y el selector indicado, o <c>null</c> si no existe.
        /// </summary>
        /// <typeparam name="TResult">El tipo del resultado proyectado.</typeparam>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <param name="selector">La expresión que define la proyección de la entidad.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>El primer resultado proyectado o <c>null</c>.</returns>
        Task<TResult?> FirstOrDefaultAsync<TResult>(
            ISpecification<T> specification,
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una lista paginada de entidades junto con el total de registros que cumplen con la especificación.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta y paginación.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
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
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        #endregion

        #region Métodos CRUD

        /// <summary>
        /// Obtiene todas las entidades de forma asíncrona.
        /// </summary>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>Una colección de entidades.</returns>
        Task<IEnumerable<T>> ToListAsync(
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega una nueva entidad al conjunto de datos de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Agrega un rango de entidades al conjunto de datos de forma asíncrona.
        /// </summary>
        /// <param name="entities">La colección de entidades a agregar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task AddRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza una entidad existente en el conjunto de datos.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualiza un rango de entidades existentes en el conjunto de datos.
        /// </summary>
        /// <param name="entities">La colección de entidades a actualizar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task UpdateRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina una entidad del conjunto de datos.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task RemoveAsync(
            T entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina un rango de entidades del conjunto de datos.
        /// </summary>
        /// <param name="entities">La colección de entidades a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task RemoveRangeAsync(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        #endregion

        #region Métodos de Consulta y Agregación Avanzados (con opción de tracking)

        /// <summary>
        /// Determina si existe alguna entidad que cumpla con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns><c>true</c> si existe alguna entidad que cumpla el predicado; de lo contrario, <c>false</c>.</returns>
        Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Determina si todas las entidades cumplen con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns><c>true</c> si todas las entidades cumplen el predicado; de lo contrario, <c>false</c>.</returns>
        Task<bool> AllAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la primera entidad que cumple con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La primera entidad que cumple el predicado.</returns>
        Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la primera entidad que cumple con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La primera entidad que cumple la especificación.</returns>
        Task<T> FirstAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la última entidad que cumple con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La última entidad que cumple el predicado.</returns>
        Task<T> LastAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la última entidad que cumple con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La última entidad que cumple la especificación.</returns>
        Task<T> LastAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la única entidad que cumple con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La única entidad que cumple el predicado.</returns>
        Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la única entidad que cumple con el predicado indicado, o <c>null</c> si no existe.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La única entidad que cumple el predicado o <c>null</c>.</returns>
        Task<T?> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el total de entidades que cumplen con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>El total de entidades que cumplen con la especificación.</returns>
        Task<long> LongCountAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el total de entidades que cumplen con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define el criterio de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>El total de entidades que cumplen con el predicado.</returns>
        Task<long> LongCountAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la entidad en la posición indicada.
        /// </summary>
        /// <param name="index">El índice de la entidad.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La entidad en la posición indicada.</returns>
        Task<T> ElementAtAsync(
            int index,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la entidad en la posición indicada o <c>null</c> si no existe.
        /// </summary>
        /// <param name="index">El índice de la entidad.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La entidad en la posición indicada o <c>null</c>.</returns>
        Task<T?> ElementAtOrDefaultAsync(
            int index,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el valor mínimo de la proyección definida en el selector para las entidades que cumplen con la especificación.
        /// </summary>
        /// <typeparam name="TResult">El tipo del valor proyectado.</typeparam>
        /// <param name="selector">La expresión que define la proyección.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>El valor mínimo de la proyección.</returns>
        Task<TResult> MinAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el valor máximo de la proyección definida en el selector para las entidades que cumplen con la especificación.
        /// </summary>
        /// <typeparam name="TResult">El tipo del valor proyectado.</typeparam>
        /// <param name="selector">La expresión que define la proyección.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>El valor máximo de la proyección.</returns>
        Task<TResult> MaxAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene la suma de los valores proyectados mediante el selector para las entidades que cumplen con la especificación.
        /// </summary>
        /// <param name="selector">La expresión que define la proyección de valores decimales.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>La suma de los valores proyectados.</returns>
        Task<decimal> SumAsync(
            Expression<Func<T, decimal>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene el promedio de los valores proyectados mediante el selector para las entidades que cumplen con la especificación.
        /// </summary>
        /// <param name="selector">La expresión que define la proyección de valores enteros.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>El promedio de los valores proyectados.</returns>
        Task<double> AverageAsync(
            Expression<Func<T, int>> selector,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Determina si la entidad especificada se encuentra en el conjunto de datos.
        /// </summary>
        /// <param name="entity">La entidad a buscar.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de la entidad consultada.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns><c>true</c> si la entidad existe; de lo contrario, <c>false</c>.</returns>
        Task<bool> ContainsAsync(
            T entity,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtiene una matriz de entidades que cumplen con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de búsqueda.</param>
        /// <param name="trackChanges">Indica si se debe realizar el tracking de las entidades consultadas.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        /// <returns>Una matriz de entidades.</returns>
        Task<T[]> ToArrayAsync(
            ISpecification<T> specification,
            bool trackChanges = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Ejecuta una operación de borrado masivo para las entidades que cumplen con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define las entidades a eliminar.</param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task ExecuteDeleteAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Ejecuta una operación de actualización masiva para las entidades que cumplen con el predicado indicado,
        /// aplicando la actualización definida en la expresión.
        /// </summary>
        /// <param name="predicate">La expresión que define las entidades a actualizar.</param>
        /// <param name="updateExpression">
        /// La expresión que define las propiedades a actualizar y sus nuevos valores.
        /// Se utiliza la clase <see cref="SetPropertyCalls{T}"/> para construir la actualización.
        /// </param>
        /// <param name="cancellationToken">Token para cancelar la operación de forma asíncrona.</param>
        Task ExecuteUpdateAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression,
            CancellationToken cancellationToken = default);

        #endregion
    }
}
