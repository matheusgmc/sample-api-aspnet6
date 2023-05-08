public class SignInRequestDTO
{
    public String email { get; set; }
    public String password { get; set; }

    public SignInRequestDTO(String email, String password)
    {
        this.email = email;
        this.password = password;
    }
}
