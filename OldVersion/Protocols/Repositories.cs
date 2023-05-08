using aspnet.Models;

namespace aspnet.Protocols;

public interface RepositoryUser
{
    public List<UserModel> getAll();

    public UserModel add(UserModel data);

    public List<UserModel> find(UserFindRequestDTO data);

    public UserModel? findByEmail(string email);

    public UserModel? findById(Guid id);
}

public abstract class RepositoryPost
{
    public abstract List<PostModel> getAll();

    public abstract PostModel add(PostModel data);

    public abstract PostModel? findByTitle(string title);

    public abstract PostModel update(PostModel data);

    public abstract PostModel? findById(Guid id);
}

public abstract class RepositoryEncrypter
{
    public abstract string hashPassword(String user_id, String password);

    public abstract bool verify(String user_id, String password, String hash);
}

public abstract class RepositoryAuthToken
{
    public abstract string createAuthToken(String user_id);
    public abstract string decodedAuthToken(String access_token);
}
