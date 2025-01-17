using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    public class VehicleActiveByOwnerSpec : Specification<Vehicle>
    {
        public VehicleActiveByOwnerSpec(int ownerId)
        {
            // 1) Combinación de filtros con LinqKit
            var ownerFilter = new VehicleByOwnerFilter(ownerId);
            var activeFilter = new ActiveFilter<Vehicle>();
            var combined = ownerFilter.And(activeFilter);

            // 2) Convertimos el filtro a Expression y lo asignamos al Criteria
            SetCriteria(combined.ToExpression());

            // 3) Incluimos la navegación con Eager Loading
            AddInclude(v => v.Owner);
            AddInclude(v => v.Accessories);

            // 4) Ordenamos por fecha de actualización descendente
            AddOrderByDescending(v => v.UpdatedAt);

            // Opcional: si quisiéramos paginación
            // ApplyPaging(0, 50); // saltar 0, tomar 50
        }
    }
}
