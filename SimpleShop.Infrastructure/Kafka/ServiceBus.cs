using KafkaFlow.Producers;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Infrastructure.Kafka;

public class ServiceBus : IServiceBus
{
    private IProducerAccessor ProducerAccessor { get; }

    public ServiceBus(IProducerAccessor producerAccessor)
    {
        ProducerAccessor = producerAccessor ?? throw new ArgumentNullException(nameof(producerAccessor));
    }

    public async Task PublishAsync<T>(T message) where T : BaseEvent
    {
    }
}