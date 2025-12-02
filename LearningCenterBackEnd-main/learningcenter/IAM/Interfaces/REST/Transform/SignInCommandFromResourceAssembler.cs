using learningcenter.IAM.Domain.Model.Commands;
using learningcenter.IAM.Interfaces.REST.Resources;

namespace learningcenter.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}