using System.Text.Json.Serialization;

namespace aspnet.Api.Core.Entities;

public class UserEntity
{
    [JsonIgnore]
    public Guid? id { get; set; } = Guid.NewGuid();

    public String username { get; set; }
    public String email { get; set; }

    [JsonIgnore]
    public String password { get; set; }

    public ICollection<PostEntity> posts { get; set; } = new List<PostEntity>();

    public UserEntity(String username, String email, String password, Guid? id = null)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
    }
}
