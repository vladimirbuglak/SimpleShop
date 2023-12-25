using FluentValidation;

namespace SimpleShop.Application.Modify.Commands.Products.Create;

public class CreateProductCommandHandlerValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandHandlerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}