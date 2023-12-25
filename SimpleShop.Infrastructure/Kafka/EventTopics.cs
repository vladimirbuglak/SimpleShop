using SimpleShop.Application.Modify.Events;

namespace SimpleShop.Infrastructure.Kafka;

public static class EventTopics
{
    public static Dictionary<Type, KafkaProducer> Mapping => new()
    {
        {typeof(ProductCreated), new KafkaProducer("product-created-producer", "product-created")},
        {typeof(ProductUpdatedEvent), new KafkaProducer("product-updated-producer", "product-updated")},
        
        {typeof(OrderCreated), new KafkaProducer("order-created-producer", "order-created")},
        {typeof(OrderCancelled), new KafkaProducer("order-cancelled-producer", "order-cancelled")},
    };
}

public class KafkaProducer
{
    public string Name { get; set; }
    
    public string TopicName { get; set; }

    public KafkaProducer(string name, string topicName)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        TopicName = topicName ?? throw new ArgumentNullException(nameof(topicName));
    }
}