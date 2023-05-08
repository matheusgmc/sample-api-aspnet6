using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Utils;

using aspnet.Api.Core.Modules.Errors.UseCases;

public class SignUpUseCase : UseCaseHandle<AuthUser, SignUpRequestDTO>
{
    private IUserRepository userRepository;
    private IAuthTokenRepository authTokenRepository;

    public SignUpUseCase(IUserRepository userRepository, IAuthTokenRepository authTokenRepository)
    {
        this.authTokenRepository = authTokenRepository;
        this.userRepository = userRepository;
    }

    public override Either<UseCaseError, AuthUser> execute(SignUpRequestDTO data)
    {
        var emailExists = this.userRepository.findByEmail(data.email);
        if (emailExists != null)
        {
            return new(left: new AlreadyExistsError("email already exists"));
        }

        var newUser = this.userRepository.add(new(data.username, data.email, data.password));

        var access_token = this.authTokenRepository.createAuthToken(newUser.id.ToString()!);

        return new(right: new(newUser, access_token));
    }
}
