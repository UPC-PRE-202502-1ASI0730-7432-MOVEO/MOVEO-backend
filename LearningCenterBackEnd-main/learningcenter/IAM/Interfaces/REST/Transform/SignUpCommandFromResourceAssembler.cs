using learningcenter.IAM.Domain.Model.Commands;
using learningcenter.IAM.Interfaces.REST.Resources;

namespace learningcenter.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}