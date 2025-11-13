using Moveo_backend.UserManagement.Domain.Model.Commands;
using Moveo_backend.UserManagement.Domain.Model.ValueObjects;
using Moveo_backend.UserManagement.Interfaces.REST.Resources;

namespace Moveo_backend.UserManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserResource> _users = new();

        public Task<IEnumerable<UserResource>> GetAllUsersAsync()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<UserResource> CreateUserAsync(CreateUserCommand command)
        {
            
            
            var user = new UserResource
            {
                Id = _users.Count + 1,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Role = command.Role,
                Phone = command.Phone,
                Dni = command.Dni,
                LicenseNumber = command.LicenseNumber,
                Address = command.Address,
                Preferences = command.Preferences
            };

            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<UserResource?> UpdateUserAsync(int id, UpdateUserCommand command)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult<UserResource?>(null);

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;
            user.Role = command.Role;
            user.Phone = command.Phone;
            user.Dni = command.Dni;
            user.LicenseNumber = command.LicenseNumber;
            user.Address = command.Address;

            // Convertimos DTO a VO
            user.Preferences = command.Preferences != null
                ? new UserPreferences(
                    command.Preferences.Language,
                    command.Preferences.EmailNotifications,
                    command.Preferences.PushNotifications,
                    command.Preferences.SmsNotifications,
                    command.Preferences.AutoAcceptRentals,
                    command.Preferences.MinimumRentalDays,
                    command.Preferences.InstantBooking
                )
                : new UserPreferences("es", true, true, false, false, 1, false);

            return Task.FromResult<UserResource?>(user);
        }


        public Task<bool> ChangePasswordAsync(int id, ChangePasswordCommand command)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult(false);

            user.Password = command.NewPassword;
            return Task.FromResult(true);
        }

        public Task<bool> ChangeUserRoleAsync(int id, ChangeUserRoleCommand command)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult(false);

            user.Role = command.NewRole;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return Task.FromResult(false);

            _users.Remove(user);
            return Task.FromResult(true);
        }
    }
}
