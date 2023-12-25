using MediatR;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Commands.Products.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private ILogger Logger { get; }

    private IDateTime DateTime { get; }

    private IServiceBus ServiceBus { get; }

    private IModifyShopContext Context { get; }


    public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IModifyShopContext context,
        IServiceBus serviceBus, IDateTime dateTime)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        DateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CreateOn = DateTime.UtcNow
        };

        Context.Products.Add(product);

        await Context.SaveChangesAsync(cancellationToken);

        await ServiceBus.PublishAsync(product.ToProductCreated());

        return product.ToProductDto();
    }
}

