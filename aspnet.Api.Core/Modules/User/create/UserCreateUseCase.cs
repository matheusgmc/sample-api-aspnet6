using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Utils;

using aspnet.Api.Core.Modules.Errors.UseCases;

public class UserCreateUseCase : UseCaseHandle<UserEntity, UserCreateRequestDTO>
{
    public IUserRepository userRepository;
    public IEncrypterRepository encrypterRepository;

    public UserCreateUseCase(
        IUserRepository userRepository,
        IEncrypterRepository encrypterRepository
    )
    {
        this.encrypterRepository = encrypterRepository;
        this.userRepository = userRepository;
    }

    public override Either<UseCaseError, UserEntity> execute(UserCreateRequestDTO data)
    {
        var isExists = this.userRepository.findByEmail(data.email);
        if (isExists != null)
        {
            return new(left: new AlreadyExistsError("email already exists"));
        }

        var user = new UserEntity(data.username, data.email, data.password);

        var hashedpassword = this.encrypterRepository.hashPassword(
            user.id.ToString()!,
            user.password
        );
        user.password = hashedpassword;

        this.userRepository.add(user);

        return new(right: user);
    }
}
