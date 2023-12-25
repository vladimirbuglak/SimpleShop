namespace SimpleShop.Application.Modify.Events;

public class BaseEvent
{
    public long Id { get; set; }

    public DateTime? CreateOn { get; set; }
}