using aspnet.Models;

namespace aspnet.Entities;

public class Post
{
    public Guid id { get; set; }
    public String title { get; set; }
    public String description { get; set; }
    public Guid user_id { get; set; }

    public Post(PostModel model)
    {
        this.title = model.title;
        this.description = model.description;
        this.user_id = model.user_id;
        this.id = model.id;
    }
}
