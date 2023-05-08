using aspnet.Protocols;
using aspnet.Helpers;
using aspnet.Entities;

public class PostUpdateController : ControllerHandle<PostUpdateRequestDTO>
{
    private UseCaseHandle<Post, PostUpdateRequestDTO> useCase;

    public PostUpdateController(UseCaseHandle<Post, PostUpdateRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle(PostUpdateRequestDTO data)
    {
        if (data.id.GetType() == null)
        {
            return new BadRequest { value = "id is required" };
        }

        if (string.IsNullOrEmpty(data.title) && string.IsNullOrEmpty(data.description))
        {
            return new BadRequest { value = "at least one property is required" };
        }

        var response = this.useCase.execute(data);

        if (response.isLeft)
        {
            return new BadRequest { value = response.left };
        }

        return new Ok { value = response.right };
    }
}
