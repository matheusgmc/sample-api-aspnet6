using aspnet.Protocols;
using aspnet.Entities;
using aspnet.Utils;

public class PostAllUseCase : UseCaseHandle<List<Post>>
{
    private RepositoryPost repositoryPost;

    public PostAllUseCase(RepositoryPost repositoryPost)
    {
        this.repositoryPost = repositoryPost;
    }

    public Either<UseCaseError, List<Post>> execute()
    {
        var listPosts = this.repositoryPost.getAll().Select(post => new Post(post)).ToList();
        return new(right: listPosts);
    }
}
