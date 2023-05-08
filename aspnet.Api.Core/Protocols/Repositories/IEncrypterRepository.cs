namespace aspnet.Api.Core.Protocols.Repositories;

public abstract class IEncrypterRepository
{
    public abstract String hashPassword(String user_id, String password);

    public abstract bool verify(String user_id, String password, String hash);
}
