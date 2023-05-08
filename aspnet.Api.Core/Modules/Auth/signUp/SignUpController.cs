using aspnet.Api.Core.Modules.Errors.Controllers;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;
using aspnet.Api.Core.Entities;

namespace aspnet.Api.Core.Modules.AuthModule;

public class SignUpController : ControllerHandle<SignUpRequestDTO>
{
    private UseCaseHandle<AuthUser, SignUpRequestDTO> useCase;

    public SignUpController(UseCaseHandle<AuthUser, SignUpRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle(SignUpRequestDTO data)
    {
        if (
            string.IsNullOrEmpty(data.username)
            || string.IsNullOrEmpty(data.password)
            || string.IsNullOrEmpty(data.email)
        )
        {
            return new BadRequest(new RequiredParamsError());
        }

        var result = this.useCase.execute(data);

        if (result.left != null)
        {
            return new BadRequest(result.left);
        }

        return new Ok(result.right!);
    }
}
