namespace aspnet.Api.Core.Protocols.Repositories;

public abstract class IAuthTokenRepository
{
    public abstract String createAuthToken(String user_id);
    public abstract String decodedAuthToken(String access_token);
}
