using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Products.GetAll
{
    public class GetProductsQuery : IRequest<List<ProductDto>> { }
}
