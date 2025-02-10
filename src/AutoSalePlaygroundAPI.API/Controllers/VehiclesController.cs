using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.AddVehicleAccessories;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.CreateVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.DeleteVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.UpdateVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwnerPaged;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetAllVehicle;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetVehicleById;
using AutoSalePlaygroundAPI.Domain.DTOs;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Constants;
using AutoSalePlaygroundAPI.CrossCutting.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.BulkUpdateVehicles;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.ExecuteVehicleUpdate;
using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Commands.PartialVehicleUpdate;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Controlador para gestionar los Autos")]
    public class VehiclesController(IMediator mediator) : ControllerBase
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

            var command = new CreateVehicleCommand(autoDto.LicensePlateNumber, autoDto.OwnerId, autoDto.Specifications.FuelType, autoDto.Specifications.EngineDisplacement, autoDto.Specifications.Horsepower);
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

            var command = new UpdateVehicleCommand(id, autoDto.Specifications.FuelType, autoDto.Specifications.EngineDisplacement, autoDto.Specifications.Horsepower);
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

        /// <summary>
        /// Obtiene la lista de vehículos activos asociados a un propietario de forma paginada (vía CQRS).
        /// </summary>
        /// <param name="ownerId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="vehicleSortByEnum"></param>
        /// <param name="orderByEnum"></param>
        /// <returns></returns>
        [HttpGet("owner/{ownerId}")]
        [SwaggerOperation(Summary = "Obtiene los autos activos de un propietario de forma paginada", Description = "Devuelve una lista paginada de autos activos asociados a un propietario.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de autos obtenida exitosamente", typeof(PaginatedResponseDto<VehicleDto>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetActiveVehiclesByOwnerPagedAsync(
            int ownerId,
            int pageNumber,
            int pageSize,
            VehicleSortByEnum vehicleSortByEnum,
            OrderByEnum orderByEnum)
        {
            var query = new GetActiveVehiclesByOwnerPagedQuery(ownerId, pageNumber, pageSize, vehicleSortByEnum, orderByEnum);
            var response = await mediator.Send(query);

            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Agrega accesorios a un auto.
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="accessoryIds"></param>
        /// <returns></returns>
        [HttpPost("{vehicleId}/accessories")]
        [SwaggerOperation(Summary = "Agrega accesorios a un auto", Description = "Añade accesorios a un auto específico.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Accesorios añadidos exitosamente", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> AddVehicleAccessories(int vehicleId, [FromBody] List<int> accessoryIds)
        {
            var command = new AddVehicleAccessoriesCommand(vehicleId, accessoryIds);
            var response = await mediator.Send(command);

            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Actualización parcial de un vehículo (actualiza solo los campos proporcionados).
        /// </summary>
        /// <param name="id">ID del vehículo a actualizar.</param>
        /// <param name="updateDto">DTO con los campos a actualizar.</param>
        [HttpPatch("{id}/partial")]
        [SwaggerOperation(Summary = "Actualiza parcialmente un auto", Description = "Actualiza únicamente los campos indicados en el DTO.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Vehículo actualizado parcialmente", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> PartialUpdateAuto(int id, [FromBody] VehiclePartialUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Opcional: asegurarse que el ID de la ruta y el del DTO coincidan
            updateDto.Id = id;

            var command = new PartialVehicleUpdateCommand(updateDto);
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
        /// Actualización en masa (bulk update) de vehículos.
        /// </summary>
        /// <param name="vehiclePartialUpdateDtos">Lista de DTOs con la información parcial a actualizar en cada vehículo.</param>
        [HttpPatch("bulk")]
        [SwaggerOperation(Summary = "Actualiza en masa varios autos", Description = "Permite actualizar campos específicos de varios vehículos en un solo request.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Vehículos actualizados en masa", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> BulkUpdateVehicles([FromBody] IEnumerable<VehiclePartialUpdateDto> vehiclePartialUpdateDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new BulkUpdateVehiclesCommand(vehiclePartialUpdateDtos);
            var response = await mediator.Send(command);

            if (!response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Actualiza un vehículo mediante el comando ExecuteVehicleUpdate.
        /// </summary>
        /// <param name="vehicleId">ID del vehículo a actualizar.</param>
        /// <param name="requestDto">DTO con los nuevos datos para el vehículo.</param>
        [HttpPatch("{vehicleId}/execute")]
        [SwaggerOperation(Summary = "Actualiza un vehículo mediante comando", Description = "Actualiza el vehículo con nuevos parámetros utilizando el comando ExecuteVehicleUpdate.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Vehículo actualizado", typeof(ResponseDto<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> ExecuteUpdateAuto(
            int vehicleId,
            [FromQuery] string NewLicensePlate,
            [FromQuery] string FuelType,
            [FromQuery] int EngineDisplacement,
            [FromQuery] int Horsepower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new ExecuteVehicleUpdateCommand(
                vehicleId,
                NewLicensePlate,
                FuelType,
                EngineDisplacement,
                Horsepower);
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
