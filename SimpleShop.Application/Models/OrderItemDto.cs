namespace SimpleShop.Application.Modify.Models;

public class OrderItemDto
{
    public decimal Price { get; set; }

    public int Quantity { get; set; }
    
    public long ProductId { get; set; }
}