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

        RuleFor(x => x.Image)
            .SetValidator(new ImageBase64CommandValidator())
            .When(x => x.Image != null);
    }
}

public class ImageBase64CommandValidator : AbstractValidator<ImageBase64Command>
{
    public ImageBase64CommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotEmpty();
    }
}