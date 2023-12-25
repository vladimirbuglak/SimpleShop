using SimpleShop.Application.Modify.Events;
using SimpleShop.Application.Modify.Models;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Modify.Extensions;

public static class OrderExtensions
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            Created = order.CreateOn,
            Status = order.Status,
            Items = order.Items.Select(x => new OrderItemDto
            {
                Price = x.Price,
                Quantity = x.Quantity,
                ProductId = x.ProductId
            }).ToList()
        };
    }

    public static OrderCreated ToOrderCreatedEvent(this Order order)
    {
        return new OrderCreated
        {
            Id = order.Id,
            CreateOn = order.CreateOn,
            Items = order.Items.Select(x => new OrderItemCreated
            {
                Id = x.Id,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateOn = x.CreateOn,
                ProductId = x.ProductId
            }).ToList()
        };
    }

    public static OrderCancelled ToOrderCancelled(this Order order)
    {
        return new OrderCancelled
        {
            Id = order.Id
        };
    }
}