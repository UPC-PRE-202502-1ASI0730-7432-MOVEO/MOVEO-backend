using learningcenter.Shared.Domain.Events;

namespace learningcenter.Publishing.Domain.Model.Events;

public class CategoryCreatedEvent(string name) : IEvent
{
    public string Name { get; } = name;
}