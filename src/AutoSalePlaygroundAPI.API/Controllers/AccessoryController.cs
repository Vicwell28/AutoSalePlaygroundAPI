using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.CreateAccessory;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.DeleteAccessory;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Commands.UpdateAccessory;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAccessoryById;
using AutoSalePlaygroundAPI.Application.CQRS.Accessory.Queries.GetAllAccessories;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("Controlador para gestionar los Accesorios")]
    public class AccessoryController : BaseApiController
    {
        public AccessoryController(IMediator mediator) : base(mediator) { }

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
            var response = await Mediator.Send(query);
            return ProcessResponse(response);
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
            var response = await Mediator.Send(query);
            return ProcessResponse(response);
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
            var command = new CreateAccessoryCommand(accessoryDto.Name);
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
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
            var command = new UpdateAccessoryCommand(id, accessoryDto.Name);
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
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
            var response = await Mediator.Send(command);
            return ProcessResponse(response);
        }
    }
}
