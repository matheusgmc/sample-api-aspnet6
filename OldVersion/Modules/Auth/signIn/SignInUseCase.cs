using aspnet.Entities;
using aspnet.Utils;
using aspnet.Protocols;

public class SignInUseCase : UseCaseHandle<AuthUser, SignInRequestDTO>
{
    private RepositoryUser repositoryUser;
    private RepositoryAuthToken repositoryAuthToken;
    private RepositoryEncrypter repositoryEncrypter;

    public SignInUseCase(
        RepositoryUser repositoryUser,
        RepositoryAuthToken repositoryAuthToken,
        RepositoryEncrypter repositoryEncrypter
    )
    {
        this.repositoryUser = repositoryUser;
        this.repositoryAuthToken = repositoryAuthToken;
        this.repositoryEncrypter = repositoryEncrypter;
    }

    public Either<UseCaseError, AuthUser> execute(SignInRequestDTO data)
    {
        var user = this.repositoryUser.findByEmail(data.email);

        if (user == null)
        {
            return new(left: new("user not found"));
        }

        if (!this.repositoryEncrypter.verify(user.id.ToString(), data.password, user.password))
        {
            return new(left: new("email or password invalid"));
        }

        var access_token = this.repositoryAuthToken.createAuthToken(user.id.ToString());

        return new(right: new(new(user), access_token));
    }
}
