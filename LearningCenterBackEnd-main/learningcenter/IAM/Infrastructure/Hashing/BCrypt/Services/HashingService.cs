using learningcenter.IAM.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace learningcenter.IAM.Infrastructure.Hashing.BCrypt.Services;

public class HashingService : IHashingService
{

    public string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordhash)
    {
        return BCryptNet.Verify(password, passwordhash);
    }
}