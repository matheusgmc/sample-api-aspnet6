using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;

using aspnet.Api.Core.Modules.Errors.Controllers;

namespace aspnet.Api.Core.Modules.UserModule;

public class UserCreateController : ControllerHandle<UserCreateRequestDTO>
{
    private UseCaseHandle<UserEntity, UserCreateRequestDTO> useCase;

    public UserCreateController(UseCaseHandle<UserEntity, UserCreateRequestDTO> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle(UserCreateRequestDTO data)
    {
        if (
            string.IsNullOrEmpty(data.username)
            || string.IsNullOrEmpty(data.email)
            || string.IsNullOrEmpty(data.password)
        )
        {
            return new BadRequest(new RequiredParamsError());
        }
        var response = this.useCase.execute(data);
        if (response.left != null)
        {
            return new BadRequest(response.left);
        }

        return new Created(response.right!);
    }
}
