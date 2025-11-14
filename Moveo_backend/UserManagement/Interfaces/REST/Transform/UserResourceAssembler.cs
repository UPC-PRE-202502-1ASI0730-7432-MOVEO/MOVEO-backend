using Moveo_backend.UserManagement.Domain.Model.Aggregates;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;

namespace Moveo_backend.UserManagement.Interfaces.REST.Transform
{
    public static class UserResourceAssembler
    {
        public static UserResource ToResourceFromEntity(User entity)
        {
            return new UserResource
            {
                Id = entity.Id,
                FirstName = entity.Name.FirstName,
                LastName = entity.Name.LastName,
                Email = entity.EmailAddress,
                Role = entity.RoleName,
                Phone = entity.Phone,
                Dni = entity.Dni,
                LicenseNumber = entity.LicenseNumber,
                Address = entity.Address,
                Preferences = entity.Preferences
            };
        }
    }
}