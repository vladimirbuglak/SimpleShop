using KafkaFlow;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Infrastructure.Persistence;

namespace SimpleShop.DbSync.Handlers;

public class ProductUpdatedHandler : IMessageHandler<ProductUpdatedEvent>
{
    private ShopDbContext Context { get; }
    
    public ProductUpdatedHandler(ShopDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task Handle(IMessageContext context, ProductUpdatedEvent message)
    {
        var product = await Context.Products.FirstAsync(x => x.Id == message.Id);

        product.Name = message.Name;
        product.Price = message.Price;
        
        Context.Products.Update(product);
        
        await Context.SaveChangesAsync();
    }
}