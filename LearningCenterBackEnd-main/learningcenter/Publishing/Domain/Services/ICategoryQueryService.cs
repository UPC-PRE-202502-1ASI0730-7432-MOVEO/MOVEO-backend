using learningcenter.Publishing.Domain.Model.Commands;
using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Domain.Model.Queries;

namespace learningcenter.Publishing.Domain.Services;

public interface ICategoryQueryService
{
    Task<Category?> Handle(GetCategoryByIdQuery command);

    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query);
}