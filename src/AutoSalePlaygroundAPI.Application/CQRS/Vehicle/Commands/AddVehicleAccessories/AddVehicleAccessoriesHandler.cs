using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.AddVehicleAccessories
{
    public class AddVehicleAccessoriesHandler : IRequestHandler<AddVehicleAccessoriesCommand, ResponseDto<VehicleDto>>
    {
        private readonly IVehicleService _vehicleService;

        public AddVehicleAccessoriesHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public Task<ResponseDto<VehicleDto>> Handle(AddVehicleAccessoriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
