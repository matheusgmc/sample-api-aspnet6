using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Helpers;

namespace aspnet.Api.Core.Modules.UserModule;

public class UserFindController : ControllerHandle<UserFindRequestDTO>
{
    public UseCaseHandle<List<UserEntity>, UserFindRequestDTO?> useCase;

    public UserFindController(UseCaseHandle<List<UserEntity>, UserFindRequestDTO?> useCase)
    {
        this.useCase = useCase;
    }

    public override Response handle(UserFindRequestDTO? data)
    {
        return new Ok(this.useCase.execute(data).right!);
    }
}
