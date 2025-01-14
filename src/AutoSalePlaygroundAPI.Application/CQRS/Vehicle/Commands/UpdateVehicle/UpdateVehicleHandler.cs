using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, ResponseDto<VehicleDto>>
    {
        public Task<ResponseDto<VehicleDto>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new NotFoundException($"Vehículo con ID {request.Id} no encontrado.");
            }

            var vehicle = new VehicleDto
            {
                Id = request.Id,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Año = request.Año,
                Precio = request.Precio
            };

            var response = ResponseDto<VehicleDto>.Success(vehicle, "Vehículo actualizado con éxito");

            return Task.FromResult(response);
        }
    }
}
