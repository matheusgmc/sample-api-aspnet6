using aspnet.Protocols;
using aspnet.Helpers;
using aspnet.Entities;

public class SignInController : ControllerHandle<SignInRequestDTO>
{
    private UseCaseHandle<AuthUser, SignInRequestDTO> useCase;

    public SignInController(UseCaseHandle<AuthUser, SignInRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle(SignInRequestDTO data)
    {
        if (string.IsNullOrEmpty(data.email) || string.IsNullOrEmpty(data.password))
        {
            return new BadRequest { value = "email or password is invalid" };
        }

        var result = this.useCase.execute(data);

        if (result.isLeft)
        {
            return new BadRequest { value = result.left };
        }

        return new Ok { value = result.right };
    }
}
