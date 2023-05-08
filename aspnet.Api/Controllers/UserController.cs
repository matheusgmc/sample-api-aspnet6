using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Modules.UserModule;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/user")]
public class UserController : ControllerBase
{
    private readonly UserCreateController _createController;
    private readonly UserFindController _findController;

    public UserController(UserCreateController createController, UserFindController findController)
    {
        this._createController = createController;
        this._findController = findController;
    }

    [HttpPost("all")]
    [ProducesResponseType(typeof(List<UserEntity>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult getAll()
    {
        var result = this._findController.handle(null);
        return this.ResponseData(result, nameof(this.addUser));
    }

    [HttpPost("add")]
    [ProducesResponseType(typeof(UserEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult addUser([FromBody] UserCreateRequestDTO data)
    {
        var result = this._createController.handle(data);
        return this.ResponseData(result, nameof(this.addUser));
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
