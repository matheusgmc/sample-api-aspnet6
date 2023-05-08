public class UserCreateRequestDTO
{
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public UserCreateRequestDTO(string username, string email, string password)
    {
        this.password = password;
        this.username = username;
        this.email = email;
    }
}
