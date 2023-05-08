using aspnet.Protocols;
using aspnet.Entities;
using aspnet.Models;
using aspnet.Utils;

public class PostUpdateUseCase : UseCaseHandle<Post, PostUpdateRequestDTO>
{
    private RepositoryPost repositoryPost;

    public PostUpdateUseCase(RepositoryPost repositoryPost)
    {
        this.repositoryPost = repositoryPost;
    }

    public Either<UseCaseError, Post> execute(PostUpdateRequestDTO data)
    {
        var postOriginal = this.repositoryPost.findById(data.id);

        if (postOriginal == null)
        {
            return new(left: new(error: "post not found"));
        }

        if (!this.shouldChangeTitle(data) && !this.shouldChangeDescription(data))
        {
            return new(left: new(error: "at least one property is required"));
        }

        postOriginal.title = this.getTitleToBeUsed(data, postOriginal);
        postOriginal.description = this.getDescriptionToBeUsed(data, postOriginal);

        this.repositoryPost.update(postOriginal);
        var postUpdate = this.repositoryPost.findById(data.id);

        if (postUpdate == null)
        {
            return new(left: new(error: "post not found"));
        }

        return new(right: new(postUpdate));
    }

    private bool shouldChangeTitle(PostUpdateRequestDTO update)
    {
        return !string.IsNullOrEmpty(update.title);
    }

    private bool shouldChangeDescription(PostUpdateRequestDTO update)
    {
        return !string.IsNullOrEmpty(update.description);
    }

    private String getTitleToBeUsed(PostUpdateRequestDTO update, PostModel original)
    {
        return this.shouldChangeTitle(update) ? update.title! : original.title;
    }

    private String getDescriptionToBeUsed(PostUpdateRequestDTO update, PostModel original)
    {
        return this.shouldChangeDescription(update) ? update.description! : original.description;
    }
}
