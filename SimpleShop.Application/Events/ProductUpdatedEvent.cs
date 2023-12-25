namespace SimpleShop.Application.Modify.Events;

public class ProductUpdatedEvent : BaseEvent
{
    public string Name { get; set; }

    public decimal Price { get; set; }
}