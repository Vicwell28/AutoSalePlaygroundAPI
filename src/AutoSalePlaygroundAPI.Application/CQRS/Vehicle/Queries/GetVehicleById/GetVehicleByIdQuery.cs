using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public record GetVehicleByIdQuery(int Id) : IRequest<ResponseDto<VehicleDto>>, IRequireValidation;
}
