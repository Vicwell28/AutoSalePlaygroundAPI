using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehiclesByOwnerPaged;
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
    [SwaggerTag("Controlador para gestionar los Autos")]
    public class VehicleController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Obtiene la lista completa de autos (vía CQRS).
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los autos", Description = "Devuelve una lista completa de autos disponibles.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de autos obtenida exitosamente", typeof(ResponseDto<List<VehicleDto>>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetAllAutos()
        {
            var query = new GetAllVehiclesQuery();
            var response = await mediator.Send(query);

            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Obtiene un auto por su ID (vía CQRS).
        /// </summary>
        /// <param name="id">ID del auto.</param>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un auto por ID", Description = "Devuelve los detalles de un auto específico según su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "VehicleDto obtenido exitosamente", typeof(ResponseDto<VehicleDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VehicleDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetAutoById(int id)
        {
            var query = new GetVehicleByIdQuery(id);
            var response = await mediator.Send(query);

            if (!response.IsSuccess)
            {
                if (response.Code == ResponseCodes.NotFound)
                    return NotFound(response);

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Crea un nuevo auto (vía CQRS).
        /// </summary>
        /// <param name="autoDto">Datos del nuevo auto.</param>
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo auto", Description = "Añade un nuevo auto a la base de datos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "VehicleDto creado exitosamente", typeof(ResponseDto<VehicleDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> CreateAuto([FromBody] VehicleDto autoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateVehicleCommand(autoDto.Marca, autoDto.Modelo, autoDto.Año, autoDto.Precio);
            var response = await mediator.Send(command);

            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un auto existente (vía CQRS).
        /// </summary>
        /// <param name="id">ID del auto a actualizar.</param>
        /// <param name="autoDto">Nuevos datos del auto.</param>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un auto", Description = "Modifica los detalles de un auto existente.")]
        [SwaggerResponse(StatusCodes.Status200OK, "VehicleDto actualizado exitosamente", typeof(ResponseDto<VehicleDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VehicleDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> UpdateAuto(int id, [FromBody] VehicleDto autoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateVehicleCommand(id, autoDto.Marca, autoDto.Modelo, autoDto.Año, autoDto.Precio);
            var response = await mediator.Send(command);

            if (!response.IsSuccess)
            {
                if (response.Code == ResponseCodes.NotFound)
                    return NotFound(response);

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Elimina un auto por su ID (vía CQRS).
        /// </summary>
        /// <param name="id">ID del auto a eliminar.</param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un auto", Description = "Elimina un auto específico según su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "VehicleDto eliminado exitosamente", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VehicleDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> DeleteAuto(int id)
        {
            var command = new DeleteVehicleCommand(id);
            var response = await mediator.Send(command);

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