using aspnet.Protocols;
using aspnet.Helpers;
using aspnet.Entities;

public class SignUpController : ControllerHandle<SignUpRequestDTO>
{
    private UseCaseHandle<AuthUser, SignUpRequestDTO> useCase;

    public SignUpController(UseCaseHandle<AuthUser, SignUpRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle(SignUpRequestDTO data)
    {
        if (
            string.IsNullOrEmpty(data.username)
            || string.IsNullOrEmpty(data.password)
            || string.IsNullOrEmpty(data.email)
        )
        {
            return new BadRequest { value = "username, password or email is invalid" };
        }

        var result = this.useCase.execute(data);

        if (result.isLeft)
        {
            return new BadRequest { value = result.left };
        }

        return new Ok { value = result.right };
    }
}
