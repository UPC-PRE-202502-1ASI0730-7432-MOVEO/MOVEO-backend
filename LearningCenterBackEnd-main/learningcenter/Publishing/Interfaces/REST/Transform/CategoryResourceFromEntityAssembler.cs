using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Interfaces.REST.Resources;

namespace learningcenter.Publishing.Interfaces.REST.Transform;

public static class CategoryResourceFromEntityAssembler
{
    public static CategoryResource ToResourceFromEntity(Category category)
    {
        return new CategoryResource(category.Id, category.Name);
        
    }
}