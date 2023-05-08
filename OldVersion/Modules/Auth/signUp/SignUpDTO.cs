public class SignUpRequestDTO
{
    public String username { get; set; }
    public String email { get; set; }
    public String password { get; set; }

    public SignUpRequestDTO(String username, String email, String password)
    {
        this.username = username;
        this.email = email;
        this.password = password;
    }
}
