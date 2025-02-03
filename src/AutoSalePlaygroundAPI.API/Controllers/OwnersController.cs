using AutoSalePlaygroundAPI.Application.CQRS.Vehicle.Queries.GetActiveVehiclesByOwnerPaged;
using AutoSalePlaygroundAPI.Application.DTOs;
using AutoSalePlaygroundAPI.Application.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ownerId}/vehicles")]
        [SwaggerOperation(
            Summary = "Obtiene vehículos de un propietario (paginados)",
            Description = "Devuelve una lista (paginada) de autos pertenecientes a un propietario específico."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista paginada de autos obtenida exitosamente", typeof(PaginatedResponseDto<VehicleDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Parámetros de paginación o ID de propietario inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public async Task<IActionResult> GetVehiclesByOwner(
            [FromRoute] int ownerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = new GetActiveVehiclesByOwnerPagedQuery(ownerId, pageNumber, pageSize);
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
