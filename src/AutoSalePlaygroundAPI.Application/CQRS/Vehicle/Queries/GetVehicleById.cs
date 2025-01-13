using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries
{
    public record GetVehicleByIdQuery(int Id) : IRequest<ResponseDto<VehicleDto>>;

    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, ResponseDto<VehicleDto>>
    {
        public Task<ResponseDto<VehicleDto>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
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
