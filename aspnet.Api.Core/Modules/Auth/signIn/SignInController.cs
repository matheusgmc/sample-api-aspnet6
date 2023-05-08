using aspnet.Api.Core.Modules.Errors.Controllers;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;
using aspnet.Api.Core.Entities;

namespace aspnet.Api.Core.Modules.AuthModule;

public class SignInController : ControllerHandle<SignInRequestDTO>
{
    private UseCaseHandle<AuthUser, SignInRequestDTO> useCase;

    public SignInController(UseCaseHandle<AuthUser, SignInRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle(SignInRequestDTO data)
    {
        if (string.IsNullOrEmpty(data.email) || string.IsNullOrEmpty(data.password))
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
