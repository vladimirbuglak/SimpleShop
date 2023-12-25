using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Commands.Products.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private ILogger Logger { get; }
    
    private IServiceBus ServiceBus { get; }

    private IModifyShopContext Context { get; }


    public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IModifyShopContext context, IServiceBus serviceBus)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await Context.Products.FirstAsync(x => x.Id == request.Id, cancellationToken);

        product.Name = request.Name;
        product.Price = request.Price;
        
        Context.Products.Update(product);
        
        await Context.SaveChangesAsync(cancellationToken);

        await ServiceBus.PublishAsync(product.ToProductUpdatedEvent());

        return product.ToProductDto();
    }
}