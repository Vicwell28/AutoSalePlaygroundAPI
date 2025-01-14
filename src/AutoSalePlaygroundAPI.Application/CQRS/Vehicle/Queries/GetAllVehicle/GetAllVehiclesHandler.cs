using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesQuery, ResponseDto<List<VehicleDto>>>
    {
        public Task<ResponseDto<List<VehicleDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = new List<VehicleDto>
            {
                new VehicleDto
                {
                    Id = 123,
                    Marca = "Toyota",
                    Modelo = "Corolla",
                    Año = 2021,
                    Precio = 25000
                },
                new VehicleDto
                {
                    Id = 124,
                    Marca = "Toyota",
                    Modelo = "Yaris",
                    Año = 2021,
                    Precio = 20000
                }
            };

            var response = ResponseDto<List<VehicleDto>>.Success(vehicles, "Vehículos obtenidos con éxito");

            return Task.FromResult(response);
        }
    }
}
