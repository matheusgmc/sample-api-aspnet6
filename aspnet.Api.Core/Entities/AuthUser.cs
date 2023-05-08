namespace aspnet.Api.Core.Entities;

public class AuthUser
{
    public UserEntity user;
    public String access_token;

    public AuthUser(UserEntity user, String access_token)
    {
        this.user = user;
        this.access_token = access_token;
    }
}
