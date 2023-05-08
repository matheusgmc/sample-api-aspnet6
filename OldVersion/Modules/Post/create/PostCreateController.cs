using aspnet.Protocols;
using aspnet.Helpers;
using aspnet.Entities;

public class PostCreateController : ControllerHandle<PostCreateRequestDTO>
{
    private UseCaseHandle<Post, PostCreateRequestDTO> useCase;

    public PostCreateController(UseCaseHandle<Post, PostCreateRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle(PostCreateRequestDTO data)
    {
        if (string.IsNullOrEmpty(data.title) || string.IsNullOrEmpty(data.description))
        {
            return new BadRequest { value = "missing params" };
        }

        var result = this.useCase.execute(data);

        if (result.isLeft)
        {
            return new BadRequest { value = result.left };
        }

        return new Created { value = result.right };
    }
}
