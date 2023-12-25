using KafkaFlow;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Persistence;

namespace SimpleShop.DbSync.Handlers;

public class OrderCreatedHandler : IMessageHandler<OrderCreated>
{
    private ShopDbContext Context { get; }
    
    public OrderCreatedHandler(ShopDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task Handle(IMessageContext context, OrderCreated message)
    {
        using (var transaction = Context.Database.BeginTransaction())
        {
            await Context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Orders] ON");
            
            var order = new Order
            {
                Id = message.Id,
                CreateOn = message.CreateOn.Value,
                Status = OrderStatus.New
            };

            Context.Orders.Add(order);

            await Context.SaveChangesAsync(CancellationToken.None);
            
            await Context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Orders] OFF");
            
            await Context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[OrderItems] ON");
            
            var items = message.Items.Select(x => new OrderItem
            {
                Id = x.Id,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateOn = x.CreateOn.Value,
                ProductId = x.ProductId,
                OrderId = order.Id
            }).ToList();
            
            Context.OrderItems.AddRange(items);
            
            await Context.SaveChangesAsync(CancellationToken.None);
            
            await Context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[OrderItems] OFF");

            await transaction.CommitAsync();
        }
    }
}