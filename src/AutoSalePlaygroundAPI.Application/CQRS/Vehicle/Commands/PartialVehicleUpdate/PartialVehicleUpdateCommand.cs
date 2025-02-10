using AutoSalePlaygroundAPI.Application.Interfaces;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.PartialVehicleUpdate
{
    /// <summary>
    /// Comando para realizar una actualización parcial de un vehículo.
    /// Se actualizarán solo los campos indicados en el DTO.
    /// </summary>
    public record PartialVehicleUpdateCommand(VehiclePartialUpdateDto UpdateDto)
        : ICommand<ResponseDto<bool>>, IRequireValidation;
}