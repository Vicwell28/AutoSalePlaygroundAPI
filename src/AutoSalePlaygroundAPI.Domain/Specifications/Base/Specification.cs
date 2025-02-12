using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications.Base
{
    /// <summary>
    /// Implementación base de la interfaz <see cref="ISpecification{T}"/> para construir consultas complejas.
    /// Permite configurar criterios (Criteria), includes, ordenamientos y paginación.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad.</typeparam>
    public class Specification<T> : ISpecification<T> where T : class, IEntity
    {
        /// <summary>
        /// Filtro principal que determina qué elementos se seleccionan.
        /// </summary>
        public virtual Expression<Func<T, bool>>? Criteria { get; protected set; }

        /// <summary>
        /// Listado de expresiones para incluir entidades relacionadas usando la sintaxis tradicional.
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new();

        /// <summary>
        /// Lista de funciones para incluir entidades relacionadas que permiten encadenar <c>ThenInclude</c>.
        /// </summary>
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }
            = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        /// <summary>
        /// Lista de funciones que aplican ordenamientos al <see cref="IQueryable{T}"/>.
        /// </summary>
        public List<Func<IQueryable<T>, IOrderedQueryable<T>>> OrderExpressions { get; } = new();

        /// <summary>
        /// Cantidad de registros a saltar para paginación.
        /// </summary>
        public int? Skip { get; protected set; }

        /// <summary>
        /// Cantidad de registros a tomar para paginación.
        /// </summary>
        public int? Take { get; protected set; }

        /// <summary>
        /// Indica si se ha configurado paginación (Skip o Take).
        /// </summary>
        public bool IsPagingEnabled => Skip.HasValue || Take.HasValue;

        /// <summary>
        /// Agrega un Include utilizando la sintaxis tradicional.
        /// </summary>
        /// <param name="includeExpression">Expresión que define la propiedad de navegación.</param>
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Agrega un Include que retorna un <see cref="IIncludableQueryable{T, object}"/> para soportar <c>ThenInclude</c>.
        /// </summary>
        /// <param name="includeExpression">Función que recibe un <see cref="IQueryable{T}"/> y retorna un <see cref="IIncludableQueryable{T, object}"/>.</param>
        public void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

        /// <summary>
        /// Asigna la expresión Criteria, normalmente utilizada en el constructor o en métodos de tipo "Where(...)". 
        /// </summary>
        /// <param name="criteria">La expresión de filtrado.</param>
        public void SetCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Agrega un ordenamiento ascendente.
        /// Si no existe un ordenamiento previo, se utiliza <c>OrderBy</c>; de lo contrario, se encadena con <c>ThenBy</c>.
        /// </summary>
        /// <typeparam name="TKey">El tipo de la clave de ordenamiento.</typeparam>
        /// <param name="keySelector">Expresión para seleccionar la clave de ordenamiento.</param>
        public void AddOrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (!OrderExpressions.Any())
            {
                OrderExpressions.Add(query => query.OrderBy(keySelector));
            }
            else
            {
                OrderExpressions.Add(query => ((IOrderedQueryable<T>)query).ThenBy(keySelector));
            }
        }

        /// <summary>
        /// Agrega un ordenamiento descendente.
        /// Si no existe un ordenamiento previo, se utiliza <c>OrderByDescending</c>; de lo contrario, se encadena con <c>ThenByDescending</c>.
        /// </summary>
        /// <typeparam name="TKey">El tipo de la clave de ordenamiento.</typeparam>
        /// <param name="keySelector">Expresión para seleccionar la clave de ordenamiento.</param>
        public void AddOrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            if (!OrderExpressions.Any())
            {
                OrderExpressions.Add(query => query.OrderByDescending(keySelector));
            }
            else
            {
                OrderExpressions.Add(query => ((IOrderedQueryable<T>)query).ThenByDescending(keySelector));
            }
        }

        /// <summary>
        /// Configura la paginación asignando la cantidad de registros a saltar y a tomar.
        /// </summary>
        /// <param name="skip">Número de registros a saltar.</param>
        /// <param name="take">Número de registros a tomar.</param>
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
