using aspnet.Protocols;
using aspnet.Entities;
using aspnet.Utils;

public class UserFindUseCase : UseCaseHandle<List<User>, UserFindRequestDTO?>
{
    private readonly RepositoryUser repositoryUser;

    public UserFindUseCase(RepositoryUser repositoryUser)
    {
        this.repositoryUser = repositoryUser;
    }

    public Either<UseCaseError, List<User>> execute(UserFindRequestDTO? data)
    {
        if (data == null)
        {
            var listUsersAll = this.repositoryUser
                .getAll()
                .Select(userModel => new User(userModel))
                .ToList();
            return new(right: listUsersAll);
        }

        var listUsers = this.repositoryUser
            .find(data)
            .Select(userModel => new User(userModel))
            .ToList();

        return new(right: listUsers);
    }
}
