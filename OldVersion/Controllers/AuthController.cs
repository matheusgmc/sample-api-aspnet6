using Microsoft.AspNetCore.Mvc;

using aspnet.Modules;
using aspnet.Entities;
using aspnet.Protocols;
using aspnet.Helpers;

namespace aspnet.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login", Name = "login")]
    [LoggerGuard(LogEvents.AuthUser, "Starting user authencation")]
    [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult SignIn([FromBody] SignInRequestDTO data)
    {
        var result = AuthModules.useSignInController.handle(data);
        return this.ResponseData(result, nameof(this.SignUp));
    }

    [HttpPost("register", Name = "register")]
    [LoggerGuard(LogEvents.RegisterUser, "Starting to register a new user")]
    [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult SignUp([FromBody] SignUpRequestDTO data)
    {
        var result = AuthModules.useSignUpController.handle(data);
        return this.ResponseData(result, nameof(this.SignUp));
    }

    private ObjectResult ResponseData(Response data, string route)
    {
        if (data.statusCode == 400)
        {
            return BadRequest(data.value);
        }

        if (data.statusCode == 201)
        {
            return Created(route, data.value);
        }
        return Ok(data.value);
    }
}
