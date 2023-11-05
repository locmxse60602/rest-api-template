using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    // Handles the given request by performing validation and calling the next handler in the pipeline.
    //
    // Parameters:
    //   request:
    //     The request object to be handled.
    //
    //   next:
    //     The delegate representing the next handler in the pipeline.
    //
    //   cancellationToken:
    //     The cancellation token that can be used to cancel the operation.
    //
    // Returns:
    //   A task that represents the asynchronous operation. The task result contains
    //   the response object.
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
