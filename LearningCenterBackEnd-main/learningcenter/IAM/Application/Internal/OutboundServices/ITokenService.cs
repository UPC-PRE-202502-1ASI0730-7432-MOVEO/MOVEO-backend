using learningcenter.IAM.Domain.Model.Aggregates;

namespace learningcenter.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    
    Task<int?> ValidateToken(string token);
    
}