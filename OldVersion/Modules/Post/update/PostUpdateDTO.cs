public class PostUpdateRequestDTO
{
    public Guid id { get; set; }
    public String? title { get; set; }
    public String? description { get; set; }

    public PostUpdateRequestDTO(String title, String description, Guid id)
    {
        this.id = id;
        this.title = title;
        this.description = description;
    }
}
