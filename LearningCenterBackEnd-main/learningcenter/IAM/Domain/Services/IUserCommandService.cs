using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.IAM.Domain.Model.Commands;

namespace learningcenter.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(User user, string token )> Handle(SignInCommand command);
    
    Task Handle(SignUpCommand command);
}