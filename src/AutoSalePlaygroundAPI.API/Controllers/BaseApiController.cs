using AutoSalePlaygroundAPI.CrossCutting.Constants;
using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoSalePlaygroundAPI.API.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        /// <summary>
        /// Centraliza el manejo de la respuesta del mediator.
        /// Retorna NotFound si el código es de recurso no encontrado; de lo contrario, en caso de error, retorna 500.
        /// </summary>
        protected IActionResult ProcessResponse<T>(ResponseDto<T> response)
        {
            if (!response.IsSuccess)
            {
                if (response.Code == ResponseCodes.NotFound)
                {
                    return NotFound(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
    }
}