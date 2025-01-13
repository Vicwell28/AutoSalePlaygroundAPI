namespace AutoSalePlaygroundAPI.API.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Leer la petición
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                var requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reiniciar el stream

                _logger.LogInformation($"[Request] Path: {context.Request.Path}, Body: {requestBody}");
            }

            // Ejecutar siguiente middleware
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // Leer la respuesta
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation($"[Response] Status Code: {context.Response.StatusCode}, Body: {text}");

                // Copiar la respuesta al stream original
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
