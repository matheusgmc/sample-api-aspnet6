using aspnet.Protocols;
using aspnet.Entities;
using aspnet.Models;
using aspnet.Utils;

public class PostCreateUseCase : UseCaseHandle<Post, PostCreateRequestDTO>
{
    private RepositoryPost repositoryPost;
    private RepositoryUser repositoryUser;

    public PostCreateUseCase(RepositoryPost repositoryPost, RepositoryUser repositoryUser)
    {
        this.repositoryUser = repositoryUser;
        this.repositoryPost = repositoryPost;
    }

    public Either<UseCaseError, Post> execute(PostCreateRequestDTO data)
    {
        var isExists = this.repositoryPost.findByTitle(data.title);
        if (isExists != null)
        {
            return new(left: new(error: "this title already exists"));
        }

        if (this.repositoryUser.findById(data.user_id) == null)
        {
            return new(left: new(error: "user not found"));
        }

        var newPost = new PostModel(data.title, data.description, data.user_id);

        this.repositoryPost.add(newPost);

        return new(right: new(newPost));
    }
}
