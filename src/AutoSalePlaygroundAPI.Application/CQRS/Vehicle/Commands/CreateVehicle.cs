using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands
{
    public record CreateVehicleCommand(string Marca, string Modelo, int Año, decimal Precio) : IRequest<ResponseDto<VehicleDto>>;

    public class CreateVehicleHandler : IRequestHandler<CreateVehicleCommand, ResponseDto<VehicleDto>>
    {
        public Task<ResponseDto<VehicleDto>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {

            var vehicle = new VehicleDto
            {
                Id = 123,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Año = request.Año,
                Precio = request.Precio
            };

            var response = ResponseDto<VehicleDto>.Success(vehicle, "Vehículo creado con éxito");

            return Task.FromResult(response);
        }
    }
}
