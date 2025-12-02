using learningcenter.Shared.Infrastructure.Persistence.EFC.Configuration;
using learningcenter.Shared.Infrastructure.Persistence.EFC.Repositories;
using learningcenter.Profiles.Domain.Model.Aggregates;
using learningcenter.Profiles.Domain.Model.ValueObjects;
using learningcenter.Profiles.Domain.Repositories;

namespace learningcenter.Profiles.Infrastruture.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().FirstOrDefault(p => p.Email == email);
    }
}