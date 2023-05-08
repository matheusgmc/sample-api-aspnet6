using aspnet.Entities;
using aspnet.Protocols;
using aspnet.Helpers;

public class UserCreateController : ControllerHandle<UserCreateRequestDTO>
{
    private UseCaseHandle<User, UserCreateRequestDTO> useCase;

    public UserCreateController(UseCaseHandle<User, UserCreateRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle(UserCreateRequestDTO data)
    {
        if (
            string.IsNullOrEmpty(data.username)
            || string.IsNullOrEmpty(data.email)
            || string.IsNullOrEmpty(data.password)
        )
        {
            return new BadRequest { value = "name, email or password is invalid" };
        }
        var response = this.useCase.execute(data);
        if (response.isLeft)
        {
            return new BadRequest { value = response.left };
        }

        return new Ok { value = response.right };
    }
}
