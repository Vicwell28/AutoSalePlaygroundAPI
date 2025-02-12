using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using AutoSalePlaygroundAPI.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Infrastructure.Interfaces
{
    /// <summary>
    /// Contrato para operaciones de lectura sobre entidades.
    /// Proporciona métodos para consultas utilizando especificaciones, proyecciones y operaciones de agregación.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public interface IReadRepository<T> where T : class, IEntity
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

        #region Métodos de Consulta Directos y Agregación

        /// <summary>
        /// Obtiene una lista de todas las entidades.
        /// </summary>
        /// <returns>Una colección de entidades.</returns>
        Task<IEnumerable<T>> ToListAsync();

        /// <summary>
        /// Determina si existe al menos una entidad que cumpla con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns><c>true</c> si existe al menos una entidad; de lo contrario, <c>false</c>.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Determina si todas las entidades cumplen con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns><c>true</c> si todas las entidades cumplen la condición; de lo contrario, <c>false</c>.</returns>
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene la primera entidad que cumple con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <returns>La primera entidad que cumple con la especificación.</returns>
        Task<T> FirstAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene la primera entidad que cumple con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns>La primera entidad que cumple con la condición.</returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene la última entidad que cumple con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <returns>La última entidad que cumple con la especificación.</returns>
        Task<T> LastAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene la última entidad que cumple con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns>La última entidad que cumple con la condición.</returns>
        Task<T> LastAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene una única entidad que cumpla con el predicado indicado.
        /// Lanza una excepción si no existe exactamente una entidad que lo cumpla.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns>La entidad que cumple con la condición.</returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene una única entidad que cumpla con el predicado indicado o <c>null</c> si no existe ninguna.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns>La entidad que cumple con la condición o <c>null</c>.</returns>
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene el número total de entidades que cumplen con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <returns>El total de entidades.</returns>
        Task<long> LongCountAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene el número total de entidades que cumplen con el predicado indicado.
        /// </summary>
        /// <param name="predicate">La expresión que define la condición.</param>
        /// <returns>El total de entidades.</returns>
        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene la entidad ubicada en el índice especificado (basado en cero).
        /// </summary>
        /// <param name="index">El índice de la entidad a obtener.</param>
        /// <returns>La entidad en la posición indicada.</returns>
        Task<T> ElementAtAsync(int index);

        /// <summary>
        /// Obtiene la entidad ubicada en el índice especificado (basado en cero) o <c>null</c> si no existe.
        /// </summary>
        /// <param name="index">El índice de la entidad a obtener.</param>
        /// <returns>La entidad en la posición indicada o <c>null</c>.</returns>
        Task<T?> ElementAtOrDefaultAsync(int index);

        /// <summary>
        /// Obtiene el valor mínimo de la proyección definida para la entidad.
        /// </summary>
        /// <typeparam name="TResult">El tipo del valor a proyectar.</typeparam>
        /// <param name="selector">La expresión que define la proyección.</param>
        /// <returns>El valor mínimo.</returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene el valor máximo de la proyección definida para la entidad.
        /// </summary>
        /// <typeparam name="TResult">El tipo del valor a proyectar.</typeparam>
        /// <param name="selector">La expresión que define la proyección.</param>
        /// <returns>El valor máximo.</returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Calcula la suma total de los valores decimales obtenidos mediante la proyección indicada.
        /// </summary>
        /// <param name="selector">La expresión que define la proyección del valor decimal.</param>
        /// <returns>La suma total.</returns>
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector);

        /// <summary>
        /// Calcula el promedio de los valores enteros obtenidos mediante la proyección indicada.
        /// </summary>
        /// <param name="selector">La expresión que define la proyección del valor entero.</param>
        /// <returns>El promedio de los valores.</returns>
        Task<double> AverageAsync(Expression<Func<T, int>> selector);

        /// <summary>
        /// Verifica si el conjunto de datos contiene la entidad indicada.
        /// </summary>
        /// <param name="entity">La entidad a buscar.</param>
        /// <returns><c>true</c> si la entidad existe; de lo contrario, <c>false</c>.</returns>
        Task<bool> ContainsAsync(T entity);

        /// <summary>
        /// Obtiene un arreglo de entidades que cumplen con la especificación indicada.
        /// </summary>
        /// <param name="specification">La especificación que define los criterios de consulta.</param>
        /// <returns>Un arreglo de entidades.</returns>
        Task<T[]> ToArrayAsync(ISpecification<T> specification);

        #endregion
    }
}
