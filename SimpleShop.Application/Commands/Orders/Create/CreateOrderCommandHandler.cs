using MediatR;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Commands.Orders.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private ILogger Logger { get; }

    private IDateTime DateTime { get; }

    private IServiceBus ServiceBus { get; }

    private IModifyShopContext Context { get; }
    
    
    public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, IDateTime dateTime, IServiceBus serviceBus, IModifyShopContext context)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        DateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CreateOn = DateTime.UtcNow,
            Status = OrderStatus.New,
            Items = request.Items.Select(x => new OrderItem
            {
                Price = x.Price,
                Quantity = x.Quantity,
                CreateOn = DateTime.UtcNow,
                ProductId = x.ProductId
            }).ToList()
        };

        Context.Orders.Add(order);

        await Context.SaveChangesAsync(cancellationToken);

        await ServiceBus.PublishAsync(order.ToOrderCreatedEvent());

        return order.ToOrderDto();
    }
}