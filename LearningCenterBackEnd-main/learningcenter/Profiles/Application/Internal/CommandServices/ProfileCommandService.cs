using learningcenter.Profiles.Domain.Model.Aggregates;
using learningcenter.Profiles.Domain.Model.Commands;
using learningcenter.Profiles.Domain.Repositories;
using learningcenter.Profiles.Domain.Services;
using learningcenter.Shared.Domain.Repositories;
namespace learningcenter.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork
    ) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
                await profileRepository.AddAsync(profile);
                await unitOfWork.CompleteAsync();
                return profile;
        }
        catch (Exception e)
        {
            return null;
        }
    }

}