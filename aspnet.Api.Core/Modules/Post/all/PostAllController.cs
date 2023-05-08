using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;

namespace aspnet.Api.Core.Modules.PostModule;

public class PostAllController : ControllerHandle
{
    private UseCaseHandle<List<PostEntity>> useCase;

    public PostAllController(UseCaseHandle<List<PostEntity>> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle()
    {
        return new Ok(this.useCase.execute().right!);
    }
}
