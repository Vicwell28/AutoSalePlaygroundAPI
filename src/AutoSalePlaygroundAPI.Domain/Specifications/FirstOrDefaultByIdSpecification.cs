using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener una entidad por su identificador.
    /// Se utiliza típicamente en operaciones que requieren obtener un único registro.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad, que debe heredar de <see cref="BaseEntity"/>.</typeparam>
    public class FirstOrDefaultByIdSpecification<T> : Specification<T>
        where T : BaseEntity
    {
        /// <summary>
        /// Inicializa la especificación utilizando el filtro por identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad.</param>
        public FirstOrDefaultByIdSpecification(int id)
        {
            var filter = new ByIdFilter<T>(id);
            SetCriteria(filter.ToExpression());
        }
    }
}
