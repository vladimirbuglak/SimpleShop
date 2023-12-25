using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Exceptions;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Orders.GetById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private ILogger Logger { get; }

    private IReadShopContext Context { get; }
    

    public GetOrderByIdQueryHandler(ILogger<GetOrderByIdQueryHandler> logger, IReadShopContext context)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await Context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (order == null) throw new NotFoundException($"Order with id: {request.Id} not found.");

        return order.ToOrderDto();
    }
}