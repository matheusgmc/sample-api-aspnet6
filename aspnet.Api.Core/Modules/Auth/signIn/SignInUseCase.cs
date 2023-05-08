using aspnet.Api.Core.Modules.Errors.UseCases;
using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Utils;

public class SignInUseCase : UseCaseHandle<AuthUser, SignInRequestDTO>
{
    private IUserRepository userRepository;
    private IAuthTokenRepository authTokenRepository;
    private IEncrypterRepository encrypterRepository;

    public SignInUseCase(
        IUserRepository userRepository,
        IAuthTokenRepository authTokenRepository,
        IEncrypterRepository encrypterRepository
    )
    {
        this.userRepository = userRepository;
        this.authTokenRepository = authTokenRepository;
        this.encrypterRepository = encrypterRepository;
    }

    public override Either<UseCaseError, AuthUser> execute(SignInRequestDTO data)
    {
        var user = this.userRepository.findByEmail(data.email);

        if (user == null)
        {
            return new(left: new NotFoundError("user not found"));
        }

        if (!this.encrypterRepository.verify(user.id.ToString()!, data.password, user.password))
        {
            return new(left: new AuthInvalidError());
        }

        var access_token = this.authTokenRepository.createAuthToken(user.id.ToString()!);

        return new(right: new(user, access_token));
    }
}
