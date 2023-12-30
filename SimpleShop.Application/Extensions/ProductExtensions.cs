using SimpleShop.Application.Modify.Events;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Extensions
{
    public static class ProductExtensions
    {
        public static ProductDto ToProductDto(this Domain.Entities.Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        public static ProductCreated ToProductCreated(this Domain.Entities.Product product)
        {
            return new ProductCreated
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CreateOn = product.CreateOn
            };
        }
        
        public static ProductUpdatedEvent ToProductUpdatedEvent(this Domain.Entities.Product product)
        {
            return new ProductUpdatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
