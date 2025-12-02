using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.IAM.Domain.Model.Commands;
using learningcenter.IAM.Domain.Model.Queries;

namespace learningcenter.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);

    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);

    Task<User?> Handle(GetUserByUsernameQuery query);
    
}