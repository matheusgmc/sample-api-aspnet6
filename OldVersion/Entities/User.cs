using aspnet.Models;

namespace aspnet.Entities;

public class User
{
    private Guid id;
    public string username { set; get; }
    public string email { set; get; }

    public List<Post> posts { get; set; }

    public User(UserModel model)
    {
        this.id = model.id;
        this.username = model.username;
        this.email = model.email;
        this.posts = model.posts.Select(post => new Post(post)).ToList();
    }

    public Guid getId()
    {
        return this.id;
    }
}
