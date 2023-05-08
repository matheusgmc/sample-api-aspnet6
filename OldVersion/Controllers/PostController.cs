using Microsoft.AspNetCore.Mvc;

using aspnet.Entities;
using aspnet.Protocols;
using aspnet.Modules;
using aspnet.Helpers;

namespace aspnet.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IHttpContextAccessor _ctx;

    public PostController(IHttpContextAccessor ctx)
    {
        this._ctx = ctx;
    }

    [HttpGet("all")]
    [LoggerGuard(LogEvents.ListItems, "Getting all posts")]
    [ProducesResponseType(typeof(List<Post>), StatusCodes.Status200OK)]
    public ObjectResult all()
    {
        var response = PostModules.useAllController.handle();
        return this.ResponseData(response, nameof(this.all));
    }

    [HttpPost("create")]
    [AuthGuard]
    [LoggerGuard(LogEvents.InsertItem, "Creating a new post")]
    [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult create([FromBody] PostCreateRequestDTO data)
    {
        var string_id = this._ctx.HttpContext?.Items["user_id"]?.ToString();

        var user_id = Guid.Parse(string_id!);

        var response = PostModules.useCreateController.handle(
            new(data.title, data.description) { user_id = user_id }
        );
        return this.ResponseData(response, nameof(this.create));
    }

    [HttpPut("update")]
    [AuthGuard]
    [LoggerGuard(LogEvents.UpdateItem, "Starting to update a post")]
    [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult update([FromBody] PostUpdateRequestDTO data)
    {
        var response = PostModules.useUpdateController.handle(data);
        return this.ResponseData(response, nameof(this.update));
    }

    private ObjectResult ResponseData(Response data, string route)
    {
        if (data.statusCode == 400)
        {
            return BadRequest(data.value);
        }
        if (data.statusCode == 201)
        {
            return Created($"api/post/{route}", data.value);
        }
        return Ok(data.value);
    }
}
