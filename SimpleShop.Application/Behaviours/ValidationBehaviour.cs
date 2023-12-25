using FluentValidation;
using MediatR;

namespace SimpleShop.Application.Modify.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private IEnumerable<IValidator<TRequest>> Validators { get; set; }

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        Validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (Validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any()) throw new RequestValidationException(failures);
        }

        return await next();
    }
}