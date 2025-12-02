using learningcenter.Publishing.Domain.Model.Events;
using learningcenter.Shared.Application.Internal.EventHandlers;

namespace learningcenter.Publishing.Application.Internal.EventHandlers;

public class CategoryCreatedEventHandler : IEventHandler<CategoryCreatedEvent>
{
    public Task Handle(CategoryCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(CategoryCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Category : {0}", domainEvent.Name);
        return Task.CompletedTask;
    }

}