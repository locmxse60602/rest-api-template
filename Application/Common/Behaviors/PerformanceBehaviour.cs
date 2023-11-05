using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _timer;

    public PerformanceBehaviour(
        ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();

        _logger = logger;
    }

    // Handles a request asynchronously and returns a response.
    //
    // Parameters:
    //   request:
    //     The request to be handled.
    //
    //   next:
    //     The delegate representing the next handler in the pipeline.
    //
    //   cancellationToken:
    //     A cancellation token that can be used to cancel the request.
    //
    // Returns:
    //     A task that represents the asynchronous operation. The task result contains the response.
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next().ConfigureAwait(false);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds <= 500)
        {
            return response;
        }

        var requestName = typeof(TRequest).Name;
        // var userId = _user.Id ?? string.Empty;
        var userName = string.Empty;

        // if (!string.IsNullOrEmpty(userId)) userName = await _identityService.GetUserNameAsync(userId);

        _logger.LogWarning(
            "Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
            requestName, elapsedMilliseconds, "USER", userName, request);

        return response;
    }
}
