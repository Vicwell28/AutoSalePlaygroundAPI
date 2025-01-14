using AutoSalePlaygroundAPI.Application.DTOs.Response;
using System.Text.Json;

namespace AutoSalePlaygroundAPI.API.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate _next, ILogger<ExceptionHandlingMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Pasa al siguiente middleware en la tubería
                await _next(context);
            }
            catch (Exception ex)
            {
                // Maneja la excepción
                _logger.LogError(ex, "Ocurrió una excepción no controlada.");

                // Envía la respuesta HTTP
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Creamos un ResponseDto<object> (o del tipo que prefieras)
            var response = ResponseDto<object>.Error(
                message: "Ha ocurrido un error inesperado.",
                errors: new List<string> { ex.Message },
                code: "ERROR"
            );

            // Configuramos el response HTTP
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            // Serializamos a JSON
            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
