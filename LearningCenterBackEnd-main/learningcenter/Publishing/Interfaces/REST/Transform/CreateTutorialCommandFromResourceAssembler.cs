using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Interfaces.REST.Resources;

namespace learningcenter.Publishing.Interfaces.REST.Transform;

public static class CreateTutorialCommandFromResourceAssembler
{
    public static CreateTutorialCommand ToCommmandFromResource(CreateTutorialResource resource)
    { 
        return new CreateTutorialCommand(resource.Title, resource.Summary, resource.CategoryId);
    }

}