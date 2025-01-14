using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AutoSalePlaygroundAPI.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Handling {RequestName} with payload {@Request}", typeof(TRequest).Name, request);

            var response = await next();

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation("Handled {RequestName} with response {@Response}", typeof(TRequest).Name, response);

            _logger.LogWarning("Long Running Request: {RequestName} ({ElapsedMilliseconds} ms)", typeof(TRequest).Name, elapsedMilliseconds);

            return response;
        }
    }
}
