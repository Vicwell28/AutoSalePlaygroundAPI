using AutoMapper;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle
{
    public class GetAllVehiclesHandler(IVehicleService vehicleService, IMapper mapper)
        : IRequestHandler<GetAllVehiclesQuery, ResponseDto<List<VehicleDto>>>
    {
        private readonly IVehicleService _vehicleService = vehicleService
            ?? throw new ArgumentNullException(nameof(vehicleService));

        private readonly IMapper _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ResponseDto<List<VehicleDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.ToListAsync();

            if (vehicles == null)
            {
                throw new InvalidDataException("No se encontraron vehículos");
            }

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);

            return ResponseDto<List<VehicleDto>>.Success(vehicleDtos, "Vehículos obtenidos con éxito");
        }
    }
}
