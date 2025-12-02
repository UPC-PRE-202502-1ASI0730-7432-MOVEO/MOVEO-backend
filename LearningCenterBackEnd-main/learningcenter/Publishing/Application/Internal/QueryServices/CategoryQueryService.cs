using learningcenter.Publishing.Domain.Model.Entities;
using learningcenter.Publishing.Domain.Model.Queries;
using learningcenter.Publishing.Domain.Repositories;
using learningcenter.Publishing.Domain.Services;

namespace learningcenter.Publishing.Application.Internal.QueryServices;

public class CategoryQueryService(ICategoryRepository categoryRepository)
    : ICategoryQueryService
{
    /// <inheritdoc />
    public async Task<Category?> Handle(GetCategoryByIdQuery query)
    {
        return await categoryRepository.FindByIdAsync(query.CategoryId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query)
    {
        return await categoryRepository.ListAsync();
    }
}