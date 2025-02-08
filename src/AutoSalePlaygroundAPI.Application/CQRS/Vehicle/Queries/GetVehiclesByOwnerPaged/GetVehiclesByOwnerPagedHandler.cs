using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwnerPaged
{
    public class GetActiveVehiclesByOwnerPagedHandler : IRequestHandler<GetActiveVehiclesByOwnerPagedQuery, PaginatedResponseDto<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public GetActiveVehiclesByOwnerPagedHandler(
            IVehicleService vehicleService,
            IMapper mapper)
        {
            _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginatedResponseDto<VehicleDto>> Handle(GetActiveVehiclesByOwnerPagedQuery request, CancellationToken cancellationToken)
        {
            var (vehicles, totalCount) = await _vehicleService.GetActiveVehiclesByOwnerPagedAsync(request.OwnerId, request.PageNumber, request.PageSize, request.VehicleSortByEnum, request.OrderByEnum);

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);

            return PaginatedResponseDto<VehicleDto>.Success(vehicleDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}
