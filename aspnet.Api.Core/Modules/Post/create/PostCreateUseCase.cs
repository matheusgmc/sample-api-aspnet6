using aspnet.Api.Core.Modules.Errors.UseCases;
using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Protocols;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Utils;

public class PostCreateUseCase : UseCaseHandle<PostEntity, PostCreateRequestDTO>
{
    private IUserRepository userRepository;
    private IPostRepository postRepository;

    public PostCreateUseCase(IUserRepository userRepository, IPostRepository postRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    public override Either<UseCaseError, PostEntity> execute(PostCreateRequestDTO data)
    {
        var isExists = this.postRepository.findByTitle(data.title);
        if (isExists != null)
        {
            return new(left: new AlreadyExistsError("there's already one post with this title"));
        }

        if (this.userRepository.findById(data.user_id) == null)
        {
            return new(left: new NotFoundError("user not found"));
        }

        var newPost = new PostEntity(data.title, data.description, data.user_id);

        this.postRepository.add(newPost);

        return new(right: newPost);
    }
}
