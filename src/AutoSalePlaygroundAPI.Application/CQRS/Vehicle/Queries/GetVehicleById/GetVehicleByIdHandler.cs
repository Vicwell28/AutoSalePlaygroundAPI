using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById
{
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, ResponseDto<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public GetVehicleByIdHandler(
            IVehicleService vehicleService,
            IMapper mapper)
        {
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDto<VehicleDto>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(request.VehicleId);

            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado");
            }

            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);

            return ResponseDto<VehicleDto>.Success(vehicleDto);
        }
    }
}
