using AutoMapper;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwner
{
    public class GetActiveVehiclesByOwnerHandler(
        IVehicleService vehicleService,
        IMapper mapper) : IRequestHandler<GetActiveVehiclesByOwnerQuery, ResponseDto<List<VehicleDto>>>
    {
        private readonly IVehicleService _vehicleService = vehicleService 
            ?? throw new ArgumentNullException(nameof(vehicleService));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ResponseDto<List<VehicleDto>>> Handle(GetActiveVehiclesByOwnerQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetActiveVehiclesByOwnerAsync(request.OwnerId);

            if (vehicles == null)
            {
                throw new InvalidDataException("Vehículos no encontrados");
            }

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);

            return ResponseDto<List<VehicleDto>>.Success(vehicleDtos, "Vehículos encontrados");
        }
    }
}
