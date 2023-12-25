using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Models;

public class OrderDto
{
    public long Id { get; set; }
    public DateTime Created { get; set; }
    
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderStatus Status { get; set; }
    
    public List<OrderItemDto> Items { get; set; }
}