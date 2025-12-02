using learningcenter.Profiles.Domain.Model.Aggregates;
using learningcenter.Profiles.Interfaces.REST.Resources;

namespace learningcenter.Profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.FullName, entity.EmailAddress, entity.StreetAddress);
    }

}