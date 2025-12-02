using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Interfaces.REST.Resources;

namespace learningcenter.Publishing.Interfaces.REST.Transform;

public static class CreateCategoryCommandFromResourceAssembler
{
    public static CreateCategoryCommand ToCommandFromResource(CreateCategoryResource resource)
    {
        return new CreateCategoryCommand(resource.Name);
    }
}