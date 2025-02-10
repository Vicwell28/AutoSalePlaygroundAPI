using AutoMapper;
using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehiclesDynamic
{
    public class GetVehiclesDynamicHandler(IVehicleService vehicleService, IMapper mapper) : IRequestHandler<GetVehiclesDynamicQuery, PaginatedResponseDto<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService = vehicleService;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResponseDto<VehicleDto>> Handle(GetVehiclesDynamicQuery request, CancellationToken cancellationToken)
        {
            var (vehicles, totalCount) = await _vehicleService.VehicleDynamicSort(request.SortCriteria, request.PageNumber, request.PageSize);

            var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);

            return PaginatedResponseDto<VehicleDto>.Success(vehicleDtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
}