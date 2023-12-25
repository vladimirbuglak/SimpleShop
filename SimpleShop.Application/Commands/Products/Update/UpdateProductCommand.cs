using System.Text.Json.Serialization;
using MediatR;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Commands.Products.Update;

public class UpdateProductCommand : IRequest<ProductDto>
{
    [JsonIgnore]
    public long Id { get; set; }
    
    public string Name { get; set; }

    public decimal Price { get; set; }
}