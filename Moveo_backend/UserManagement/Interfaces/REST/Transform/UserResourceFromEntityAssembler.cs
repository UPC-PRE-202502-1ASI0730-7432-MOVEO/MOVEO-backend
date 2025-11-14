using Moveo_backend.UserManagement.Domain.Model.Aggregates;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;

namespace Moveo_backend.UserManagement.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.EmailAddress,
            Role = user.RoleName,
            Phone = user.Phone,
            Dni = user.Dni,
            LicenseNumber = user.LicenseNumber,
            Address = user.Address,
            Preferences = user.Preferences
        };
    }
}