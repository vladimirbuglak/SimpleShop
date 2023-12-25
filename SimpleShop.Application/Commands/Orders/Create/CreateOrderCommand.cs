using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Commands.Orders.Create;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public List<CrateOrderItem> Items { get; set; }
}

public class CrateOrderItem
{
    public decimal Price { get; set; }

    public int Quantity { get; set; }
    
    public long ProductId { get; set; }
}