using AutoSalePlaygroundAPI.Application.DTOs.Response;
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
                // Ejecuta el siguiente Behavior o el Handler final
                var response = await next();

                return response;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Excepción de validación en {RequestName}", typeof(TRequest).Name);

                // Como TResponse hereda de BaseResponseDto, podemos construir uno
                var errorResponse = new TResponse();

                errorResponse.SetError(
                    message: "Error de validación",
                    errors: ex.Errors.Select(e => e.ErrorMessage).ToList(),
                    code: "ERR-VALIDATION"
                );

                return errorResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción en {RequestName}", typeof(TRequest).Name);

                // Como TResponse hereda de BaseResponseDto, podemos construir uno
                var errorResponse = new TResponse();

                errorResponse.SetError(
                    message: "Error durante la ejecución del request",
                    errors: new List<string> { ex.Message },
                    code: "ERR-UNHANDLED"
                );

                return errorResponse;
            }
        }
    }
}
