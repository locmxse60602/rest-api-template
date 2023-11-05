using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    // Processes the given request asynchronously.
    // 
    // Parameters:
    //   request: The request object to be processed.
    //   cancellationToken: A cancellation token that can be used to cancel the operation.
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userName = await _authenticationService.GetCurrentUser();

        _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, "USER", userName, request);
    }
}
