using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.IAM.Domain.Model.Queries;
using learningcenter.IAM.Domain.Repositories;
using learningcenter.IAM.Domain.Services;

namespace learningcenter.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }
}