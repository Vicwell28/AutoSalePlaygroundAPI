using AutoSalePlaygroundAPI.Domain.DTOs.Response;
using AutoSalePlaygroundAPI.CrossCutting.Constants;
using AutoSalePlaygroundAPI.CrossCutting.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AutoSalePlaygroundAPI.Application.Behaviors
{
    public class ExceptionHandlingBehavior<TRequest, TResponse>(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : BaseResponseDto, new()
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var response = await next();

                return response;
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Recurso no encontrado");

                var errorResponse = new TResponse();

                errorResponse.SetError(
                    message: ex.Message,
                    errors: new List<string> { ex.Message },
                    code: ResponseCodes.NotFound);

                return errorResponse;
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex, "Conflicto en la operación");

                var errorResponse = new TResponse();

                errorResponse.SetError(
                    message: ex.Message,
                    errors: new List<string> { ex.Message },
                    code: ResponseCodes.Conflict);

                return errorResponse;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Excepción de validación en {RequestName}", typeof(TRequest).Name);

                var errorResponse = new TResponse();

                errorResponse.SetError(
                    message: "Error de validación",
                    errors: ex.Errors.Select(e => e.ErrorMessage).ToList(),
                    code: ResponseCodes.ValidationError
                );

                return errorResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción en {RequestName}", typeof(TRequest).Name);

                var errorResponse = new TResponse();

                errorResponse.SetError(
                    message: "Error durante la ejecución del request",
                    errors: new List<string> { ex.Message },
                    code: ResponseCodes.UnexpectedError
                );

                return errorResponse;
            }
        }
    }
}
