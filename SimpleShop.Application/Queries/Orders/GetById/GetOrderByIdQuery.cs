using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Orders.GetById;

public class GetOrderByIdQuery : IRequest<OrderDto>
{
    public GetOrderByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}
