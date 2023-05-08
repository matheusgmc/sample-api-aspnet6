using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Utils;

public class UserFindUseCase : UseCaseHandle<List<UserEntity>, UserFindRequestDTO?>
{
    private readonly IUserRepository userRepository;

    public UserFindUseCase(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public override Either<UseCaseError, List<UserEntity>> execute(UserFindRequestDTO? data)
    {
        if (data == null)
        {
            var listUsersAll = this.userRepository.getAll().ToList();
            return new(right: listUsersAll);
        }

        var listUsers = this.userRepository.find(new(data.username)).ToList();

        return new(right: listUsers);
    }
}
