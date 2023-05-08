using aspnet.Protocols;
using aspnet.Entities;
using aspnet.Models;
using aspnet.Utils;

public class UserCreateUseCase : UseCaseHandle<User, UserCreateRequestDTO>
{
    public RepositoryUser repository;
    public RepositoryEncrypter repositoryEncrypter;

    public UserCreateUseCase(RepositoryUser repository, RepositoryEncrypter repositoryEncrypter)
    {
        this.repositoryEncrypter = repositoryEncrypter;
        this.repository = repository;
    }

    public Either<UseCaseError, User> execute(UserCreateRequestDTO data)
    {
        var isExists = this.repository.findByEmail(data.email);
        if (isExists != null)
        {
            return new(left: new(error: "email already exists"));
        }

        var user = new UserModel(data.username, data.email, data.password);

        var hashedpassword = this.repositoryEncrypter.hashPassword(
            user.id.ToString(),
            data.password
        );
        user.password = hashedpassword;

        this.repository.add(user);

        return new(right: new(user));
    }
}
