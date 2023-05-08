using aspnet.Implements;

namespace aspnet.Modules;

public class AuthModules
{
    private static JwtAuthImplement useJwtAuthImplement = new JwtAuthImplement();
    private static SqLiteUserImplement useSqLiteUserImplement = new SqLiteUserImplement();
    private static EncrypterImplement useEncrypterImplement = new EncrypterImplement();

    // Module to register a user;
    public static SignUpUseCase useSignUpUseCase = new SignUpUseCase(
        UserModules.useCreateUseCase,
        useJwtAuthImplement
    );
    public static SignUpController useSignUpController = new SignUpController(useSignUpUseCase);

    // Module to login user
    public static SignInUseCase useSignInUseCase = new SignInUseCase(
        useSqLiteUserImplement,
        useJwtAuthImplement,
        useEncrypterImplement
    );
    public static SignInController useSignInController = new SignInController(useSignInUseCase);
}
