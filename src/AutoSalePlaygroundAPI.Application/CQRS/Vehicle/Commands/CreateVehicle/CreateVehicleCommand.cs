using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle
{
    public record CreateVehicleCommand(string Marca, string Modelo, int Año, decimal Precio)
        : ICommand<ResponseDto<VehicleDto>>, IRequireValidation;
}
