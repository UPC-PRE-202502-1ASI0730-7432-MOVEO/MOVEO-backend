using learningcenter.Profiles.Domain.Model.Aggregates;
using learningcenter.Profiles.Domain.Model.Commands;

namespace learningcenter.Profiles.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
}