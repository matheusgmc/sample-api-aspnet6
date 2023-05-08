using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Modules.PostModule;

using Microsoft.AspNetCore.Mvc;

using aspnet.Api.Guards;

[ApiController]
[Route("api/v1/post")]
public class PostController : ControllerBase
{
    private PostCreateController _createController;
    private PostAllController _allController;
    private PostUpdateController _updateController;
    private IHttpContextAccessor _ctx;

    public PostController(
        IHttpContextAccessor ctx,
        PostCreateController createController,
        PostAllController allController,
        PostUpdateController updateController
    )
    {
        this._ctx = ctx;
        this._createController = createController;
        this._allController = allController;
        this._updateController = updateController;
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(List<PostEntity>), StatusCodes.Status200OK)]
    public ObjectResult all()
    {
        var response = this._allController.handle();
        return this.ResponseData(response, nameof(this.all));
    }

    [HttpPost("create")]
    [AuthGuard]
    [ProducesResponseType(typeof(PostEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult create([FromBody] PostCreateRequestDTO data)
    {
        var string_id = this._ctx.HttpContext?.Items["user_id"]?.ToString();

        var user_id = Guid.Parse(string_id!);

        var response = this._createController.handle(new(data.title, data.description, user_id));
        return this.ResponseData(response, nameof(this.create));
    }

    [HttpPut("update")]
    [AuthGuard]
    [ProducesResponseType(typeof(PostEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UseCaseError), StatusCodes.Status400BadRequest)]
    public ObjectResult update([FromBody] PostUpdateRequestDTO data)
    {
        var response = this._updateController.handle(data);
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
