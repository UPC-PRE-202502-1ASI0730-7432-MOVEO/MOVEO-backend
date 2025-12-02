using learningcenter.Publishing.Domain.Model.Aggregate;
using learningcenter.Publishing.Domain.Model.Queries;
using learningcenter.Shared.Domain.Repositories;

namespace learningcenter.Publishing.Domain.Repositories;

public interface ITutorialRepository : IBaseRepository<Tutorial>
{
    Task<IEnumerable<Tutorial>> FindByCategoryIdAsync(int CategoryId);
    
    Task<bool> ExistsByTitleAsync(string title);
    
}