using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace learningcenter.Publishing.Interfaces.REST.Transform;

public static class TutorialResourceFromEntityAssembler
{
    public static TutorialResource ToResourceFromEntity(Tutorial tutorial)
    {
        return new TutorialResource(
            tutorial.Id,
            tutorial.Title,
            tutorial.Summary,
            CategoryResourceFromEntityAssembler.ToResourceFromEntity(tutorial.Category),
           tutorial.Status.GetDisplayName() 
        );

    }
}