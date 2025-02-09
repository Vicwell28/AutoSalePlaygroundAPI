using AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.CreateOwner;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.DeleteOwner;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetAllOwners;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetOwnerById;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Controlador para gestionar los Propietarios")]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Obtiene todos los propietarios",
            Description = "Devuelve una lista completa de propietarios activos."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de propietarios obtenida exitosamente", typeof(ResponseDto<List<OwnerDto>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetAllOwners()
        {
            var query = new GetAllOwnersQuery();
            var response = await _mediator.Send(query);
            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene un propietario por ID",
            Description = "Devuelve los detalles de un propietario específico según su ID."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "OwnerDto obtenido exitosamente", typeof(ResponseDto<OwnerDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "OwnerDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            var query = new GetOwnerByIdQuery(id);
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
            Summary = "Crea un nuevo propietario",
            Description = "Añade un nuevo propietario a la base de datos."
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "OwnerDto creado exitosamente", typeof(ResponseDto<OwnerDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerDto ownerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new CreateOwnerCommand(ownerDto.FirstName, ownerDto.LastName);
            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Actualiza un propietario",
            Description = "Modifica los detalles de un propietario existente."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "OwnerDto actualizado exitosamente", typeof(ResponseDto<OwnerDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "OwnerDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody] OwnerDto ownerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new UpdateOwnerCommand(id, ownerDto.FirstName, ownerDto.LastName);
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
            Summary = "Elimina un propietario",
            Description = "Elimina (o desactiva) un propietario específico según su ID."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "OwnerDto eliminado exitosamente", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "OwnerDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var command = new DeleteOwnerCommand(id);
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
