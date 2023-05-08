using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Modules.Errors.UseCases;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Utils;

public class PostUpdateUseCase : UseCaseHandle<PostEntity, PostUpdateRequestDTO>
{
    private IPostRepository postRepository;

    public PostUpdateUseCase(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public override Either<UseCaseError, PostEntity> execute(PostUpdateRequestDTO data)
    {
        var postOriginal = this.postRepository.findById(data.post_id);

        if (postOriginal == null)
        {
            return new(left: new NotFoundError("post not found"));
        }

        if (postOriginal.user_id != data.user_id)
        {
            return new(left: new NotBelongError("this post does not belong to the given user"));
        }

        postOriginal.title = this.getTitleToBeUsed(data, postOriginal);
        postOriginal.description = this.getDescriptionToBeUsed(data, postOriginal);

        this.postRepository.update(postOriginal);
        var postUpdate = this.postRepository.findById(data.post_id);

        if (postUpdate == null)
        {
            return new(left: new NotFoundError("post not found"));
        }

        return new(right: postUpdate);
    }

    private bool shouldChangeTitle(PostUpdateRequestDTO update)
    {
        return !string.IsNullOrEmpty(update.title);
    }

    private bool shouldChangeDescription(PostUpdateRequestDTO update)
    {
        return !string.IsNullOrEmpty(update.description);
    }

    private String getTitleToBeUsed(PostUpdateRequestDTO update, PostEntity original)
    {
        return this.shouldChangeTitle(update) ? update.title! : original.title;
    }

    private String getDescriptionToBeUsed(PostUpdateRequestDTO update, PostEntity original)
    {
        return this.shouldChangeDescription(update) ? update.description! : original.description;
    }
}
