using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleShop.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Column(TypeName = "nvarchar(24)")]
        public OrderStatus Status { get; set; }
        
        public List<OrderItem> Items { get; set; }
    }
}
