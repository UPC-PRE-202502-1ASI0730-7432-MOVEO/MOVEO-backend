using learningcenter.IAM.Application.Internal.OutboundServices;
using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.IAM.Domain.Model.Commands;
using learningcenter.IAM.Domain.Repositories;
using learningcenter.IAM.Domain.Services;
using learningcenter.Shared.Domain.Repositories;

namespace learningcenter.IAM.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user= await userRepository.FindByUsernameAsync(command.Username);
        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");
        
        var token = tokenService.GenerateToken(user);
        return (user, token);

    }

    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception("Username already exists");
        
        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);

        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error creating user", e);
        }
    }
}