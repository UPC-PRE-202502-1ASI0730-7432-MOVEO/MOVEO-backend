using Cortex.Mediator.Notifications;
using learningcenter.Shared.Domain.Events;

namespace learningcenter.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}