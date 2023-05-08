namespace aspnet.Models;

public class PostModel
{
    public Guid id { get; set; }
    public String title { get; set; }
    public String description { get; set; }

    public Guid user_id { get; set; }
    public UserModel user { get; set; } = null!;

    public PostModel(String title, String description, Guid user_id)
    {
        this.title = title;
        this.description = description;
        this.user_id = user_id;
        this.id = Guid.NewGuid();
    }

    public PostModel(String title, String description, Guid user_id, Guid id)
    {
        this.title = title;
        this.description = description;
        this.user_id = user_id;
        this.id = id;
    }
}
