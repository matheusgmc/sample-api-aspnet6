namespace aspnet.Models;

public class UserModel
{
    public Guid id { get; set; }
    public String username { get; set; }
    public String email { get; set; }
    public String password { get; set; }

    public ICollection<PostModel> posts { get; set; }

    public UserModel(String username, String email, String password)
    {
        this.username = username;
        this.email = email;
        this.password = password;
        this.id = Guid.NewGuid();
        this.posts = new List<PostModel>();
    }

    public UserModel(
        String username,
        String email,
        String password,
        Guid id,
        ICollection<PostModel> posts
    )
    {
        this.username = username;
        this.email = email;
        this.password = password;
        this.id = id;
        this.posts = posts;
    }
}
