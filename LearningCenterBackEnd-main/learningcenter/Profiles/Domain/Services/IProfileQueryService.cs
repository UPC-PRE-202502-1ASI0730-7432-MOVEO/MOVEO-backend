using learningcenter.Profiles.Domain.Model.Aggregates;
using learningcenter.Profiles.Domain.Model.Queries;

namespace learningcenter.Profiles.Domain.Services;

public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    
    Task<Profile?> Handle(GetProfileByIdQuery query);
    
    Task<Profile?> Handle(GetProfileByEmailQuery query);
}