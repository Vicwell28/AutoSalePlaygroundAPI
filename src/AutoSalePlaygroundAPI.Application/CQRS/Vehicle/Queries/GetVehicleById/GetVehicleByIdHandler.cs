using AutoMapper;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public class GetVehicleByIdHandler(
        IVehicleService vehicleService,
        IMapper mapper) : IRequestHandler<GetVehicleByIdQuery, ResponseDto<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService = vehicleService 
            ?? throw new ArgumentNullException(nameof(vehicleService));
        
        private readonly IMapper _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<ResponseDto<VehicleDto>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(request.VehicleId);

            if (vehicle == null)
            {
                throw new NotFoundException("Vehículo no encontrado");
            }

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return ResponseDto<VehicleDto>.Success(vehicleDto);
        }
    }
}
