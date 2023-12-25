using SimpleShop.Application.Modify.Events;

namespace SimpleShop.Application.Modify.Interfaces;

public interface IServiceBus
{
    Task PublishAsync<T>(T message) where T : BaseEvent;
}