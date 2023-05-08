using aspnet.Api.Core.Modules.UserModule;
using aspnet.Api.Core.Modules.PostModule;
using aspnet.Api.Core.Modules.AuthModule;

using aspnet.Api.Core.Protocols.Repositories;

using aspnet.Api.Infra.AuthToken;

public static class ControllerServices
{
    private static Controller controller = new Controller();

    public static IServiceCollection useControllerServices(this IServiceCollection builder)
    {
        builder.AddSingleton<UserFindController>(
            controller.Make<UserFindController, UserFindUseCase>()
        );

        builder.AddSingleton<UserCreateController>(
            controller.Make<UserCreateController, UserCreateUseCase>()
        );

        builder.AddSingleton<PostAllController>(
            controller.Make<PostAllController, PostAllUseCase>()
        );

        builder.AddSingleton<PostCreateController>(
            controller.Make<PostCreateController, PostCreateUseCase>()
        );

        builder.AddSingleton<PostUpdateController>(
            controller.Make<PostUpdateController, PostUpdateUseCase>()
        );

        builder.AddSingleton<SignInController>(controller.Make<SignInController, SignInUseCase>());
        builder.AddSingleton<SignUpController>(controller.Make<SignUpController, SignUpUseCase>());

        builder.AddScoped<IAuthTokenRepository, AuthTokenRepository>();

        return builder;
    }
}

class Controller
{
    private ControllerFactory useFactory = new ControllerFactory();

    public TController Make<TController, TUseCase>()
    {
        var controller = this.useFactory.Build<TController, TUseCase>();
        return controller;
    }
}
