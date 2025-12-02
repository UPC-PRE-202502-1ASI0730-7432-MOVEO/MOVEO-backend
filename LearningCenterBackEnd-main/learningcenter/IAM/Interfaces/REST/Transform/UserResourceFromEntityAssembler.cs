using learningcenter.IAM.Domain.Model.Aggregates;
using learningcenter.IAM.Interfaces.REST.Resources;

namespace learningcenter.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}