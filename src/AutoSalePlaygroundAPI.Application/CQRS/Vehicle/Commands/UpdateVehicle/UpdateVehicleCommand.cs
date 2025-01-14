﻿using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.Application.Interfaces;
using MediatR;

namespace AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(int Id, string Marca, string Modelo, int Año, decimal Precio) : IRequest<ResponseDto<VehicleDto>>, IRequireValidation;
}