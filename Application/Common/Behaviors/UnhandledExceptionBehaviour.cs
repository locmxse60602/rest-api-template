using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    // Handles a request and returns a response asynchronously.
    //
    // Parameters:
    //   request: The request object of type TRequest.
    //   next: The delegate that represents the next request handler in the pipeline.
    //   cancellationToken: A cancellation token that can be used to cancel the request.
    //
    // Returns:
    //   A Task of type TResponse representing the asynchronous operation.
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
