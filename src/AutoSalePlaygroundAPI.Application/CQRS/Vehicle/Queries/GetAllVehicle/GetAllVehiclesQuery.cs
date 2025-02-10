using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle
{
    public record GetAllVehiclesQuery 
        : IQuery<ResponseDto<List<VehicleDto>>>;
}
