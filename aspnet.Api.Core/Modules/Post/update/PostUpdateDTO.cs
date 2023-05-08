public class PostUpdateRequestDTO
{
    public Guid post_id { get; set; }
    public Guid user_id { get; set; }
    public String? title { get; set; } = null;
    public String? description { get; set; } = null;

    public PostUpdateRequestDTO(
        Guid post_id,
        Guid user_id,
        String? title = null,
        String? description = null
    )
    {
        this.post_id = post_id;
        this.user_id = user_id;
        this.title = title;
        this.description = description;
    }
}
