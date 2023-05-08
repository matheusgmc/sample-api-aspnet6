using aspnet.Api.Core.Entities;

namespace aspnet.Api.Core.Protocols.Repositories;

public class IUserRepositoryFindData
{
    public String? username { get; set; }

    public IUserRepositoryFindData(String? username = null)
    {
        this.username = username;
    }
}

public abstract class IUserRepository
{
    public abstract List<UserEntity> getAll();

    public abstract UserEntity add(UserEntity data);

    public abstract List<UserEntity> find(IUserRepositoryFindData data);

    public abstract UserEntity? findByEmail(String email);

    public abstract UserEntity? findById(Guid id);
}
