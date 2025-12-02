using learningcenter.Profiles.Domain.Model.Aggregates;
using learningcenter.Profiles.Domain.Model.ValueObjects;
using learningcenter.Shared.Domain.Repositories;

namespace learningcenter.Profiles.Domain.Repositories;

public interface IProfileRepository: IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByEmailAsync(EmailAddress email);
}