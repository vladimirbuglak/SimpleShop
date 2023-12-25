using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Commands.Orders.Cancel;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private ILogger Logger { get; }

    private IServiceBus ServiceBus { get; }

    private IModifyShopContext Context { get; }
    

    public CancelOrderCommandHandler(ILogger<CancelOrderCommandHandler> logger, IServiceBus serviceBus, IModifyShopContext context)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await Context.Orders.FirstAsync(x => x.Id == request.Id, cancellationToken);

        order.Status = OrderStatus.Cancelled;

        await Context.SaveChangesAsync(cancellationToken);

        await ServiceBus.PublishAsync(order.ToOrderCancelled());
    }
}