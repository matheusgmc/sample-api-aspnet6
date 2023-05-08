using aspnet.Implements;

namespace aspnet.Modules;

public class PostModules
{
    private static SqLitePostImplement useSqLitePostImplement = new SqLitePostImplement();
    private static SqLiteUserImplement useSqLiteUserImplement = new SqLiteUserImplement();

    // Module to get all posts;
    public static PostAllUseCase useAllUseCase = new PostAllUseCase(useSqLitePostImplement);
    public static PostAllController useAllController = new PostAllController(useAllUseCase);

    // Module to create a new post;
    public static PostCreateUseCase useCreateUseCase = new PostCreateUseCase(
        useSqLitePostImplement,
        useSqLiteUserImplement
    );
    public static PostCreateController useCreateController = new PostCreateController(
        useCreateUseCase
    );

    // Module to update a post;
    public static PostUpdateUseCase useUpdateUseCase = new PostUpdateUseCase(
        useSqLitePostImplement
    );
    public static PostUpdateController useUpdateController = new PostUpdateController(
        useUpdateUseCase
    );
}
