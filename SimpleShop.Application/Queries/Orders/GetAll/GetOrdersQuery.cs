using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Orders.GetAll;

public class GetOrdersQuery : IRequest<List<OrderDto>> { }