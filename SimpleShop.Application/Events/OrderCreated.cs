namespace SimpleShop.Application.Modify.Events;

public class OrderCreated : BaseEvent
{
    public List<OrderItemCreated> Items { get; set; }
}

public class OrderItemCreated : BaseEvent
{
    public long ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}