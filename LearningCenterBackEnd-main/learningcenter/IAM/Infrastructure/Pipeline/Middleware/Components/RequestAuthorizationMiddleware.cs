using learningcenter.IAM.Application.Internal.OutboundServices;
using learningcenter.IAM.Domain.Model.Queries;
using learningcenter.IAM.Domain.Services;
using learningcenter.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace learningcenter.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata.Any(m  => m.GetType() == typeof(AllowAnonymousAttribute));

        if (allowAnonymous)
        {
            Console.WriteLine("Skipping Authorization");
            await next(context);
            return;
        }
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (token ==null) throw new Exception("Null or empty token");

        var userId = await tokenService.ValidateToken(token);
        
        if (userId == null) throw new Exception("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        var user = await userQueryService.Handle(getUserByIdQuery);
        Console.WriteLine("Successful authorization, updating context");
        context.Items["user"] = user;
        Console.WriteLine("Continuing with middleware pipeline");
        await next(context);
    }
}