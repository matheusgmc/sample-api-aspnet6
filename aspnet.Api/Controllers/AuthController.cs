using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Modules.AuthModule;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private SignInController _signInController;
    private SignUpController _signUpController;

    public AuthController(SignInController signInController, SignUpController signUpController)
    {
        this._signInController = signInController;
        this._signUpController = signUpController;
    }

    [HttpPost("login", Name = "login")]
    [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult SignIn([FromBody] SignInRequestDTO data)
    {
        var result = this._signInController.handle(data);
        return this.ResponseData(result, nameof(this.SignUp));
    }

    [HttpPost("register", Name = "register")]
    [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult SignUp([FromBody] SignUpRequestDTO data)
    {
        var result = this._signUpController.handle(data);
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
