using Microsoft.AspNetCore.Mvc;

using aspnet.Modules;
using aspnet.Entities;
using aspnet.Protocols;
using aspnet.Helpers;

namespace aspnet.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpGet("all", Name = "all")]
    [LoggerGuard(LogEvents.ListItems, "Getting All Users")]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    public ObjectResult get()
    {
        var result = UserModules.useFindController.handle(null);

        return this.ResponseData(result, nameof(this.get));
    }

    [HttpGet()]
    [LoggerGuard(LogEvents.InsertItem, "Finding a User")]
    [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
    public ObjectResult find([FromQuery] UserFindRequestDTO data)
    {
        var result = UserModules.useFindController.handle(data);
        return this.ResponseData(result, nameof(this.find));
    }

    [HttpPost("add", Name = "add")]
    [LoggerGuard(LogEvents.InsertItem, "Creating a new User")]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult addUser([FromBody] UserCreateRequestDTO data)
    {
        var result = UserModules.useCreateController.handle(data);

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
