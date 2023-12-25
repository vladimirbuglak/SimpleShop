using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Products.GetById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public GetProductByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
