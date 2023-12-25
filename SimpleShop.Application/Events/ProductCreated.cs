namespace SimpleShop.Application.Modify.Events;

public class ProductCreated : BaseEvent
{
    public string Name { get; set; }

    public decimal Price { get; set; }
}