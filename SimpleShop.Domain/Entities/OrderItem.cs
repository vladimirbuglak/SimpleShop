namespace SimpleShop.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public long ProductId { get; set; }
        
        public Product Product { get; set; }
        
        public long OrderId { get; set; }
        
        public Order Order { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
