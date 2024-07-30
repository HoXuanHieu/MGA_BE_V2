using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Net;
using Web_API.ResponseModel;

namespace Web_API;

public class AuthenticationMiddlewareHandlerService : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _handler = new AuthorizationMiddlewareResultHandler();
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Challenged)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsJsonAsync(new ApiResponse<Boolean>("", false, 401));
            return;
        }
        if (authorizeResult.Forbidden)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsJsonAsync(new ApiResponse<Boolean>("", false, 403));
            return;
        }

        await _handler.HandleAsync(next, context, policy, authorizeResult);
    }
}
