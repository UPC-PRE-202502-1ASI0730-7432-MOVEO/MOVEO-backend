using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Domain.Model.Entities;

namespace learningcenter.Publishing.Domain.Services;

public interface ICategoryCommandService
{
    public Task<Category?> Handle(CreateCategoryCommand command);
}