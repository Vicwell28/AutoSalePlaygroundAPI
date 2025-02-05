﻿using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.DeleteAccessory;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAccessoryById;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAllAccessories;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Controlador para gestionar los Accesorios")]
    public class AccessoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccessoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene todos los accesorios",
            Description = "Devuelve una lista completa de accesorios activos."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de accesorios obtenida exitosamente", typeof(ResponseDto<List<AccessoryDto>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetAllAccessories()
        {
            var query = new GetAllAccessoriesQuery();
            var response = await _mediator.Send(query);
            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene un accesorio por ID",
            Description = "Devuelve los detalles de un accesorio específico según su ID."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "AccessoryDto obtenido exitosamente", typeof(ResponseDto<AccessoryDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "AccessoryDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetAccessoryById(int id)
        {
            var query = new GetAccessoryByIdQuery(id);
            var response = await _mediator.Send(query);
            if (!response.IsSuccess)
            {
                if (response.Code == ResponseCodes.NotFound)
                    return NotFound(response);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un nuevo accesorio",
            Description = "Añade un nuevo accesorio a la base de datos."
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "AccessoryDto creado exitosamente", typeof(ResponseDto<AccessoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> CreateAccessory([FromBody] AccessoryDto accessoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new CreateAccessoryCommand(accessoryDto.Name);
            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un accesorio",
            Description = "Modifica los detalles de un accesorio existente."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "AccessoryDto actualizado exitosamente", typeof(ResponseDto<AccessoryDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "AccessoryDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> UpdateAccessory(int id, [FromBody] AccessoryDto accessoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new UpdateAccessoryCommand(id, accessoryDto.Name);
            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
            {
                if (response.Code == ResponseCodes.NotFound)
                    return NotFound(response);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Elimina un accesorio",
            Description = "Elimina un accesorio específico según su ID."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "AccessoryDto eliminado exitosamente", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "AccessoryDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> DeleteAccessory(int id)
        {
            var command = new DeleteAccessoryCommand(id);
            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
            {
                if (response.Code == ResponseCodes.NotFound)
                    return NotFound(response);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
    }
}
