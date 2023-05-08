namespace aspnet.Helpers;

using Protocols;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthGuard : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext ctx)
    {
        var authorization_header = ctx.HttpContext.Request.Headers[
            "Authorization"
        ].FirstOrDefault();

        if (string.IsNullOrEmpty(authorization_header))
        {
            ctx.Result = new UnauthorizedObjectResult(
                new { error = "Authorization header is required" }
            );
            return;
        }

        var parts = authorization_header.Split(" ");
        if (parts.Length != 2 && parts[0] == "Bearer")
        {
            ctx.Result = new UnauthorizedObjectResult(new { error = "Bearer Token is malformed" });
            return;
        }

        var jwtService = ctx.HttpContext.RequestServices.GetService<RepositoryAuthToken>();

        if (jwtService == null)
        {
            ctx.Result = new JsonResult(new { error = "jwt service is not implemented" })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
            return;
        }

        ctx.HttpContext.Items["user_id"] = jwtService.decodedAuthToken(parts[1]);
    }
}
