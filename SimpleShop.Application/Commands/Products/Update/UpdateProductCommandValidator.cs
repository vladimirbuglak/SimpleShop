using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Application.Modify.Commands.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IModifyShopContext context)
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .MustAsync(async (id, token) => await context.Products.AnyAsync(p => p.Id == id, token))
            .WithMessage(x => $"Product with id: {x.Id} not found");
    }
}
    