using aspnet.Implements;

namespace aspnet.Modules;

public class UserModules
{
    private static SqLiteUserImplement useSqLiteUserImplement = new SqLiteUserImplement();
    private static EncrypterImplement useEncrypterImplement = new EncrypterImplement();

    public static UserCreateUseCase useCreateUseCase = new UserCreateUseCase(
        useSqLiteUserImplement,
        useEncrypterImplement
    );
    public static UserCreateController useCreateController = new UserCreateController(
        useCreateUseCase
    );

    public static UserFindUseCase useFindUseCase = new UserFindUseCase(useSqLiteUserImplement);
    public static UserFindController useFindController = new UserFindController(useFindUseCase);
}
