using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Queries;

namespace learningcenter.Publishing.Domain.Services;

public interface ITutorialQueryService
{
    Task<Tutorial?> Handle(GetTutorialByIdQuery query);
    
    Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsQuery query);
    
    Task<IEnumerable<Tutorial>> Handle(GetAllTutorialsByCategoryIdQuery query);
}