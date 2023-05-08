using System.Text.Json.Serialization;

public class PostCreateRequestDTO
{
    public String title { get; set; }
    public String description { get; set; }

    [JsonIgnore]
    public Guid user_id;

    public PostCreateRequestDTO(String title, String description, Guid user_id)
    {
        this.title = title;
        this.description = description;
        this.user_id = user_id;
    }
}
