using aspnet.Api.Core.Entities;

namespace aspnet.Api.Core.Protocols.Repositories;

public abstract class IPostRepository
{
    public abstract List<PostEntity> getAll();

    public abstract PostEntity add(PostEntity data);

    public abstract PostEntity? findByTitle(String title);
    public abstract PostEntity? findById(Guid id);

    public abstract PostEntity? update(PostEntity data);
}
