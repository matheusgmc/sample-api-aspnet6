using aspnet.Protocols;
using aspnet.Entities;
using aspnet.Utils;

public class SignUpUseCase : UseCaseHandle<AuthUser, SignUpRequestDTO>
{
    private UserCreateUseCase useUserCreateUseCase;
    private RepositoryAuthToken repositoryAuthToken;

    public SignUpUseCase(
        UserCreateUseCase useUserCreateUseCase,
        RepositoryAuthToken repositoryAuthToken
    )
    {
        this.repositoryAuthToken = repositoryAuthToken;
        this.useUserCreateUseCase = useUserCreateUseCase;
    }

    public Either<UseCaseError, AuthUser> execute(SignUpRequestDTO data)
    {
        var newUserOrError = this.useUserCreateUseCase.execute(
            new(data.username, data.email, data.password)
        );

        if (newUserOrError.isLeft)
        {
            return new(left: newUserOrError.left!);
        }

        var user = newUserOrError.right!;

        var access_token = this.repositoryAuthToken.createAuthToken(user.getId().ToString());

        return new(right: new(user, access_token));
    }
}
