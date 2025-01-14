using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, ResponseDto<VehicleDto>>
    {
        public Task<ResponseDto<VehicleDto>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new NotFoundException($"Vehículo con ID {request.Id} no encontrado.");
            }

            var vehicle = new VehicleDto
            {
                Id = request.Id,
                Marca = "Ford",
                Modelo = "Fiesta",
                Año = 2022,
                Precio = 10000
            };

            var response = ResponseDto<VehicleDto>.Success(vehicle, "Vehículo encontrado con éxito");

            return Task.FromResult(response);
        }
    }
}
