namespace aspnet.Api.Core.Entities;

public class PostEntity
{
    public Guid id { get; set; } = Guid.NewGuid();

    public String title { get; set; }
    public String description { get; set; }

    public Guid user_id { get; set; }
    public UserEntity? user { get; set; } = null!;

    public PostEntity(String title, String description, Guid user_id)
    {
        this.title = title;
        this.description = description;
        this.user_id = user_id;
    }
}
