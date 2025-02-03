using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace AutoSalePlaygroundAPI.Infrastructure.Interfaces
{
    /// <summary>
    /// Define el contrato para el repositorio genérico que gestiona operaciones de acceso a datos.
    /// Permite realizar operaciones CRUD, consultas mediante especificaciones, agregaciones y operaciones en masa.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que implementa IEntity.</typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        // --- Métodos de Consulta mediante ISpecification ---
        /// <summary>
        /// Obtiene una lista de entidades que cumplen la especificación.
        /// </summary>
        Task<List<T>> ListAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene el primer elemento que cumple la especificación o null si no existe.
        /// </summary>
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);

        /// <summary>
        /// Proyecta la lista de entidades a un tipo TResult, según la especificación y el selector indicado.
        /// </summary>
        Task<List<TResult>> ListAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Proyecta el primer elemento que cumple la especificación a un tipo TResult o null si no existe.
        /// </summary>
        Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Retorna una tupla con la lista de entidades y el total de registros (útil para paginación).
        /// </summary>
        Task<(List<T> Data, int TotalCount)> ListPaginatedAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        // --- Métodos CRUD ---
        /// <summary>
        /// Agrega una nueva entidad de forma asíncrona.
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Agrega un conjunto de entidades de forma asíncrona.
        /// </summary>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Actualiza una entidad.
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Actualiza un conjunto de entidades.
        /// </summary>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Elimina una entidad.
        /// </summary>
        Task RemoveAsync(T entity);

        /// <summary>
        /// Elimina un conjunto de entidades.
        /// </summary>
        Task RemoveRangeAsync(IEnumerable<T> entities);

        // --- Métodos de Consulta y Agregación Avanzados ---
        /// <summary>
        /// Verifica si existe al menos un elemento que cumpla el predicado.
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si todos los elementos cumplen el predicado.
        /// </summary>
        Task<bool> AllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene el primer elemento que cumple la especificación (lanza excepción si no existe).
        /// </summary>
        Task<T> FirstAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene el primer elemento que cumple el predicado (lanza excepción si no existe).
        /// </summary>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene el último elemento que cumple la especificación (requiere un ordenamiento definido).
        /// </summary>
        Task<T> LastAsync(ISpecification<T> specification);

        /// <summary>
        /// Obtiene el último elemento que cumple el predicado (requiere un ordenamiento definido).
        /// </summary>
        Task<T> LastAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene un único elemento que cumpla el predicado (lanza excepción si hay más de uno).
        /// </summary>
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene un único elemento que cumpla el predicado o null si no existe.
        /// </summary>
        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Devuelve la cantidad de elementos según la especificación, en formato long.
        /// </summary>
        Task<long> LongCountAsync(ISpecification<T> specification);

        /// <summary>
        /// Devuelve la cantidad de elementos según el predicado, en formato long.
        /// </summary>
        Task<long> LongCountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Obtiene el elemento en la posición indicada (utiliza Skip + First).
        /// </summary>
        Task<T> ElementAtAsync(int index);

        /// <summary>
        /// Obtiene el elemento en la posición indicada o null si no existe.
        /// </summary>
        Task<T?> ElementAtOrDefaultAsync(int index);

        /// <summary>
        /// Obtiene el valor mínimo basado en el selector.
        /// </summary>
        Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Obtiene el valor máximo basado en el selector.
        /// </summary>
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector);

        /// <summary>
        /// Calcula la suma de los valores obtenidos mediante el selector.
        /// </summary>
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector);

        /// <summary>
        /// Calcula el promedio de los valores obtenidos mediante el selector.
        /// </summary>
        Task<double> AverageAsync(Expression<Func<T, int>> selector);

        /// <summary>
        /// Verifica si la entidad existe en el conjunto.
        /// </summary>
        Task<bool> ContainsAsync(T entity);

        /// <summary>
        /// Proyecta la consulta a un arreglo de entidades.
        /// </summary>
        Task<T[]> ToArrayAsync(ISpecification<T> specification);

        // --- Operaciones en Masa (Alias para EF Core 9) ---
        /// <summary>
        /// Ejecuta una operación de borrado masivo para todas las entidades que cumplen el predicado.
        /// </summary>
        Task ExecuteDeleteAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Ejecuta una operación de actualización masiva para todas las entidades que cumplen el predicado.
        /// </summary>
        Task ExecuteUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
    }
}
