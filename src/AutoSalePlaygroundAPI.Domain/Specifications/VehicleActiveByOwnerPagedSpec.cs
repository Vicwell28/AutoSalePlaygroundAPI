using AutoSalePlaygroundAPI.CrossCutting.Enum;
using AutoSalePlaygroundAPI.Domain.Entities;
using AutoSalePlaygroundAPI.Domain.Specifications.Base;
using AutoSalePlaygroundAPI.Domain.Specifications.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoSalePlaygroundAPI.Domain.Specifications
{
    /// <summary>
    /// Especificación para obtener vehículos activos de un propietario con paginación.
    /// </summary>
    public class VehicleActiveByOwnerPagedSpec : Specification<Vehicle>
    {
        public VehicleActiveByOwnerPagedSpec(
            int ownerId,
            int pageNumber,
            int pageSize,
            VehicleSortByEnum vehicleSortByEnum,
            OrderByEnum orderByEnum)
        {
            // 1) Combinación de filtros con LinqKit
            var ownerFilter = new VehicleByOwnerFilter(ownerId);
            var activeFilter = new ActiveFilter<Vehicle>();
            var combined = ownerFilter.And(activeFilter);

            SetCriteria(combined.ToExpression());

            AddInclude(vehiclesQuery => vehiclesQuery
                .Include(v => v.Owner)
                .Include(v => v.Accessories)
                );

            if (orderByEnum == OrderByEnum.Ascending)
            {
                Expression<Func<Vehicle, object>> sortExpression = vehicleSortByEnum switch
                {
                    VehicleSortByEnum.Id => v => v.Id,
                    VehicleSortByEnum.CreatedAt => v => v.CreatedAt,
                    VehicleSortByEnum.OwnerId => v => v.OwnerId,
                    VehicleSortByEnum.EngineDisplacement => v => v.Specifications.EngineDisplacement,
                    VehicleSortByEnum.Horsepower => v => v.Specifications.Horsepower,
                    _ => v => v.Id
                };

                AddOrderBy(sortExpression);
            }
            else
            {
                Expression<Func<Vehicle, object>> sortExpression = vehicleSortByEnum switch
                {
                    VehicleSortByEnum.Id => v => v.Id,
                    VehicleSortByEnum.CreatedAt => v => v.CreatedAt,
                    VehicleSortByEnum.OwnerId => v => v.OwnerId,
                    VehicleSortByEnum.EngineDisplacement => v => v.Specifications.EngineDisplacement,
                    VehicleSortByEnum.Horsepower => v => v.Specifications.Horsepower,
                    _ => v => v.Id
                };

                AddOrderByDescending(sortExpression);
            }

            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
