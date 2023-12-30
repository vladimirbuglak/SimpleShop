using KafkaFlow.Producers;
using SimpleShop.Application.Modify.Events;
using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Infrastructure.Kafka;

public class ServiceBus : IServiceBus
{
    public async Task PublishAsync<T>(T message) where T : BaseEvent
    {
    }
}