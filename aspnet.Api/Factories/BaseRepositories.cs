using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Infra.Database.Repositories;
using aspnet.Api.Infra.AuthToken;
using aspnet.Api.Infra.Encrypter;

public class BaseRepositories
{
    public static IUserRepository useUserRepository { get; } = new SqLiteUserRepository();
    public static IPostRepository usePostRepository { get; } = new SqLitePostRepository();

    public static IAuthTokenRepository useAuthTokenRepository { get; } = new AuthTokenRepository();
    public static IEncrypterRepository useEncrypterRepository { get; } = new EncrypterRepository();
}
