using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Modules.Errors.Controllers;

namespace aspnet.Api.Core.Modules.PostModule;

public class PostCreateController : ControllerHandle<PostCreateRequestDTO>
{
    private UseCaseHandle<PostEntity, PostCreateRequestDTO> useCase;

    public PostCreateController(UseCaseHandle<PostEntity, PostCreateRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle(PostCreateRequestDTO data)
    {
        if (string.IsNullOrEmpty(data.title) || string.IsNullOrEmpty(data.description))
        {
            return new BadRequest(new RequiredParamsError());
        }

        var result = this.useCase.execute(data);

        if (result.left != null)
        {
            return new BadRequest(result.left);
        }

        return new Created(result.right!);
    }
}
