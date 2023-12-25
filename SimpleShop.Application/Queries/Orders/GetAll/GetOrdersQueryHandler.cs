using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Orders.GetAll;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private ILogger Logger { get; }

    private IReadShopContext Context { get; }
    

    public GetOrdersQueryHandler(ILogger<GetOrdersQueryHandler> logger, IReadShopContext context)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await Context.Orders
            .Include(x => x.Items)
            .ToListAsync(cancellationToken);

        return orders.Select(x => x.ToOrderDto()).ToList();
    }
}