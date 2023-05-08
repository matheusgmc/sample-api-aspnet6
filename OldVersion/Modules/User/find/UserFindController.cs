using aspnet.Entities;
using aspnet.Protocols;
using aspnet.Helpers;

public class UserFindController : ControllerHandle<UserFindRequestDTO>
{
    public UseCaseHandle<List<User>, UserFindRequestDTO?> useCase;

    public UserFindController(UseCaseHandle<List<User>, UserFindRequestDTO?> useCase)
    {
        this.useCase = useCase;
    }

    public Response handle(UserFindRequestDTO? data)
    {
        return new Ok { value = this.useCase.execute(data).right };
    }
}
