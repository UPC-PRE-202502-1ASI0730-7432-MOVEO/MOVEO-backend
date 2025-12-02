using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Queries;
using learningcenter.Publishing.Domain.Repositories;
using learningcenter.Publishing.Domain.Services;

namespace learningcenter.Publishing.Application.Internal.QueryServices;

public class TutorialQueryService(ITutorialRepository tutorialRepository) : ITutorialQueryService
{
    public async Task<Tutorial?> Handle(GetTutorialByIdQuery query)
    {
        
        return await tutorialRepository.FindByIdAsync(query.TutorialId);
    }

    public async Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsQuery query)
    {
        return await tutorialRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsByCategoryIdQuery query)
    {
        return await tutorialRepository.FindByCategoryIdAsync(query.CategoryId);
    }
    
}