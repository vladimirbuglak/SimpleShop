using MediatR;

namespace SimpleShop.Application.Modify.Commands.Orders.Cancel;

public class CancelOrderCommand : IRequest
{
    public long Id { get; set; }

    public CancelOrderCommand(long id)
    {
        Id = id;
    }
}