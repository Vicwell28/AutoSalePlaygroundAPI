using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle
{
    public record GetAllVehiclesQuery : IQuery<ResponseDto<List<VehicleDto>>>;
}
