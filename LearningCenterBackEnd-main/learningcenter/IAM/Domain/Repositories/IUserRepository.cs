using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.Shared.Domain.Repositories;

namespace learningcenter.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    
    bool ExistsByUsername(string username);
}