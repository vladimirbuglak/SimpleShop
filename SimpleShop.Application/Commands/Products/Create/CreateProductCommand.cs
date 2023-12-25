using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Commands.Products.Create
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
