using System.Text.Json.Serialization;

public class PostCreateRequestDTO
{
    public String title { get; set; }
    public String description { get; set; }

    [JsonIgnore]
    public Guid user_id { get; set; }

    public PostCreateRequestDTO(String title, String description)
    {
        this.title = title;
        this.description = description;
    }
}
