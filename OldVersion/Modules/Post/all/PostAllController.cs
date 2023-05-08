using aspnet.Entities;
using aspnet.Protocols;
using aspnet.Helpers;

public class PostAllController : ControllerHandle
{
    private UseCaseHandle<List<Post>> useCase;

    public PostAllController(UseCaseHandle<List<Post>> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle()
    {
        return new Ok { value = this.useCase.execute().right };
    }
}
