using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle
{
    public class CreateVehicleHandler(IMapper _mapper) : IRequestHandler<CreateVehicleCommand, ResponseDto<VehicleDto>>
    {
        public Task<ResponseDto<VehicleDto>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Handler not implemented");

            //// Ejemplo de una entidad en memoria
            //var vehicleEntity = new Vehicle
            //{
            //    // En la vida real, Id lo generas en la DB
            //    Marca = request.Marca,
            //    Modelo = request.Modelo,
            //    Año = request.Año,
            //    Precio = request.Precio
            //};

            //// Devuelves un VehicleDto usando AutoMapper
            //var dto = _mapper.Map<VehicleDto>(vehicleEntity);

            //var response = ResponseDto<VehicleDto>.Success(dto, "Vehículo creado con éxito");

            //return Task.FromResult(response);
        }
    }
}
