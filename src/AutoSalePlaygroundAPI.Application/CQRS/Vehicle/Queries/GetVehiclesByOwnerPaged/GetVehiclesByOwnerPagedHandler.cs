using AutoMapper;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Domain.Specifications;
using AutoSalePlaygroundAPI.Infrastructure.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehiclesByOwnerPaged
{
    public class GetVehiclesByOwnerPagedHandler
    : IRequestHandler<GetVehiclesByOwnerPagedQuery, PaginatedResponseDto<VehicleDto>>
    {
        private readonly IRepository<Domain.Entities.Vehicle> _vehicleRepository;
        private readonly IMapper _mapper;

        public GetVehiclesByOwnerPagedHandler(IRepository<Domain.Entities.Vehicle> vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponseDto<VehicleDto>> Handle(
            GetVehiclesByOwnerPagedQuery request,
            CancellationToken cancellationToken)
        {
            // 1) Construimos la specification que ya incluye la paginación
            var spec = new VehicleActiveByOwnerPagedSpec(
                request.OwnerId,
                request.PageNumber,
                request.PageSize
            );

            // 2) Invocamos al repositorio
            var (vehicles, totalCount) = await _vehicleRepository
                .ListPaginatedAsync(spec, cancellationToken);

            // 3) Mapear a DTO (si es que tienes Vehicle -> VehicleDto)
            var vehicleDtos = _mapper.Map<List<VehicleDto>>(vehicles);

            // 4) Construimos el PaginatedResponseDto (o tu propia estructura)
            var response = PaginatedResponseDto<VehicleDto>.Success(
                data: vehicleDtos,
                currentPage: request.PageNumber,
                pageSize: request.PageSize,
                totalCount: totalCount,
                message: "Vehículos paginados obtenidos con éxito"
            );

            return response;
        }
    }
}