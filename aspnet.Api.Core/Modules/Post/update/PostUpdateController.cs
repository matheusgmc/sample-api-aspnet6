using aspnet.Api.Core.Modules.Errors.Controllers;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;
using aspnet.Api.Core.Entities;

namespace aspnet.Api.Core.Modules.PostModule;

public class PostUpdateController : ControllerHandle<PostUpdateRequestDTO>
{
    private UseCaseHandle<PostEntity, PostUpdateRequestDTO> useCase;

    public PostUpdateController(UseCaseHandle<PostEntity, PostUpdateRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle(PostUpdateRequestDTO data)
    {
        if (data.post_id.GetType() == null)
        {
            return new BadRequest(new RequiredParamsError("post id is required"));
        }

        if (data.user_id.GetType() == null)
        {
            return new BadRequest(new RequiredParamsError("user id is required"));
        }

        if (string.IsNullOrEmpty(data.title) && string.IsNullOrEmpty(data.description))
        {
            return new BadRequest(new RequiredParamsError("at least one property is required"));
        }

        var response = this.useCase.execute(data);

        if (response.left != null)
        {
            return new BadRequest(response.left);
        }

        return new Ok(response.right!);
    }
}
