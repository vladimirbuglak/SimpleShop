using KafkaFlow;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Persistence;

namespace SimpleShop.DbSync.Handlers;

public class OrderCancelledHandler : IMessageHandler<OrderCancelled>
{
    private ShopDbContext Context { get; }
    
    public OrderCancelledHandler(ShopDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task Handle(IMessageContext context, OrderCancelled message)
    {
        var order = await Context.Orders.FirstAsync(x => x.Id == message.Id);

        order.Status = OrderStatus.Cancelled;

        await Context.SaveChangesAsync();
    }
}