using AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.CreateOwner;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.DeleteOwner;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Commands.UpdateOwner;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetAllOwners;
using AutoSalePlaygroundAPI.Application.CQRS.Owner.Queries.GetOwnerById;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("Controlador para gestionar los Propietarios")]
    public class OwnerController : BaseApiController
    {
        public OwnerController(IMediator mediator) : base(mediator) { }

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
            var response = await Mediator.Send(query);
            return ProcessResponse(response);
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
            var response = await Mediator.Send(query);
            return ProcessResponse(response);
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
            var command = new CreateOwnerCommand(ownerDto.FirstName, ownerDto.LastName);
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
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
            var command = new UpdateOwnerCommand(id, ownerDto.FirstName, ownerDto.LastName);
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
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
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
        }
    }
}
