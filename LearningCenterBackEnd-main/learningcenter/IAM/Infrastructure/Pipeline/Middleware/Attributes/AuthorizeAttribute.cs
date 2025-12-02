using learningcenter.IAM.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace learningcenter.IAM.Infrastructure.Pipeline.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            Console.WriteLine("AllowAnonymous Skipping authorization");
            return;
        }
        
        var user= (User?) context.HttpContext.Items["user"];
        if (user == null) context.Result = new UnauthorizedResult();
        
    }
}