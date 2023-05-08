using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Utils;

public class PostAllUseCase : UseCaseHandle<List<PostEntity>>
{
    private IPostRepository postRepository;

    public PostAllUseCase(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public override Either<UseCaseError, List<PostEntity>> execute()
    {
        var listPosts = this.postRepository.getAll().ToList();
        return new(right: listPosts);
    }
}
