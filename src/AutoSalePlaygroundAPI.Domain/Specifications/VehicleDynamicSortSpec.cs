using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Enum;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para ordenar vehículos de forma dinámica utilizando múltiples criterios.
    /// Permite definir un conjunto de criterios de ordenamiento, incluir relaciones y aplicar paginación.
    /// </summary>
    public class VehicleDynamicSortSpec : Specification<Vehicle>
    {
        /// <summary>
        /// Inicializa la especificación con la lista de criterios de ordenamiento y los parámetros de paginación.
        /// </summary>
        /// <param name="sortCriteria">Lista de criterios de ordenamiento.</param>
        /// <param name="pageNumber">El número de página (1-based).</param>
        /// <param name="pageSize">La cantidad de registros por página.</param>
        public VehicleDynamicSortSpec(List<SortCriteriaDto> sortCriteria, int pageNumber, int pageSize)
        {
            // Filtro base: por ejemplo, solo vehículos activos.
            SetCriteria(v => v.IsActive);

            // Incluir relaciones necesarias.
            AddInclude(v => v.Owner);
            AddInclude(v => v.Accessories);

            // Aplicar dinámicamente los criterios de ordenamiento.
            if (sortCriteria != null && sortCriteria.Any())
            {
                foreach (var criteria in sortCriteria)
                {
                    switch (criteria.SortField)
                    {
                        case VehicleSortByEnum.CreatedAt:
                            if (criteria.SortDirection == OrderByEnum.Ascending)
                                AddOrderBy(v => v.CreatedAt);
                            else
                                AddOrderByDescending(v => v.CreatedAt);
                            break;
                        case VehicleSortByEnum.OwnerId:
                            if (criteria.SortDirection == OrderByEnum.Ascending)
                                AddOrderBy(v => v.OwnerId);
                            else
                                AddOrderByDescending(v => v.OwnerId);
                            break;
                        case VehicleSortByEnum.EngineDisplacement:
                            if (criteria.SortDirection == OrderByEnum.Ascending)
                                AddOrderBy(v => v.Specifications.EngineDisplacement);
                            else
                                AddOrderByDescending(v => v.Specifications.EngineDisplacement);
                            break;
                        case VehicleSortByEnum.Horsepower:
                            if (criteria.SortDirection == OrderByEnum.Ascending)
                                AddOrderBy(v => v.Specifications.Horsepower);
                            else
                                AddOrderByDescending(v => v.Specifications.Horsepower);
                            break;
                        default:
                            // Ordenamiento por defecto: por Id.
                            if (criteria.SortDirection == OrderByEnum.Ascending)
                                AddOrderBy(v => v.Id);
                            else
                                AddOrderByDescending(v => v.Id);
                            break;
                    }
                }
            }
            else
            {
                // Si no se proporcionan criterios, se aplica un ordenamiento por defecto.
                AddOrderBy(v => v.Id);
            }

            // Aplicar paginación: calcular el "skip" según la página y el tamaño.
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
