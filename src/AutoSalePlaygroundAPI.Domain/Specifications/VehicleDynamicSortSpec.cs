using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Enum;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class VehicleDynamicSortSpec : Specification<Vehicle>
    {
        public VehicleDynamicSortSpec(List<SortCriteriaDto> sortCriteria, int pageNumber, int pageSize)
        {
            // Filtro base: por ejemplo, vehículos activos.
            SetCriteria(v => v.IsActive);

            // Incluir relaciones que necesitemos (Owner, Accessories, etc.)
            AddInclude(v => v.Owner);
            AddInclude(v => v.Accessories);

            // Aplicar dinámicamente los criterios de ordenamiento
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
                            // Ordenamiento por defecto: por Id
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
                // Si no se proporcionan criterios, se aplica un ordenamiento por defecto
                AddOrderBy(v => v.Id);
            }

            // Aplicar paginación: calcular el "skip" según la página y el tamaño
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
