using KafkaFlow;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Infrastructure.Persistence;

namespace SimpleShop.DbSync.Handlers;

public class ProductCreatedHandler : IMessageHandler<ProductCreated>
{
    private ShopDbContext Context { get; }
    
    public ProductCreatedHandler(ShopDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task Handle(IMessageContext context, ProductCreated message)
    {
        using (var transaction = Context.Database.BeginTransaction())
        {
            await Context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");

            Context.Products.Add(new Domain.Entities.Product
            {
                Id = message.Id,
                Name = message.Name,
                Price = message.Price,
                CreateOn = message.CreateOn.Value
            });

            await Context.SaveChangesAsync(CancellationToken.None);

            await Context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

            await transaction.CommitAsync();
        }
    }
}