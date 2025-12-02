using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.IAM.Domain.Repositories;
using learningcenter.Shared.Infrastructure.Persistence.EFC.Configuration;
using learningcenter.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace learningcenter.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context) , IUserRepository
{

    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user=> user.Username == username);
        
    }

    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }
}