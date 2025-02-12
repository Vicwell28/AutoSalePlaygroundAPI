using AutoSalePlaygroundAPI.Domain.Entities;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Interfaces
{
    /// <summary>
    /// Contrato para filtros que pueden transformarse en una expresión lambda.
    /// Se utiliza para definir criterios de filtrado y para combinarlos mediante operaciones lógicas.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad sobre la que se aplica el filtro.</typeparam>
    public interface IFilter<T> where T : class, IEntity
    {
        /// <summary>
        /// Convierte el filtro en una expresión lambda que representa un predicado de filtrado.
        /// </summary>
        /// <returns>Una expresión de tipo <see cref="Expression{Func{T, bool}}"/> que se puede usar en consultas LINQ.</returns>
        Expression<Func<T, bool>> ToExpression();
    }
}
