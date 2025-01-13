using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands
{
    public record UpdateVehicleCommand(int Id, string Marca, string Modelo, int Año, decimal Precio) : IRequest<ResponseDto<VehicleDto>>;

    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, ResponseDto<VehicleDto>>
    {
        public Task<ResponseDto<VehicleDto>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                return Task.FromResult(ResponseDto<VehicleDto>.Error(
                       message: "Vehículo no encontrado",
                       errors: new List<string> { $"Vehicle con ID {request.Id} no existe" },
                       code: "NOT_FOUND"
                   ));
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
