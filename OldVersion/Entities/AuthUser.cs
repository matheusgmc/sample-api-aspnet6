namespace aspnet.Entities;

public class AuthUser
{
    public User user { get; set; }
    public String access_token { get; set; }

    public AuthUser(User user, String access_token)
    {
        this.user = user;
        this.access_token = access_token;
    }
}
