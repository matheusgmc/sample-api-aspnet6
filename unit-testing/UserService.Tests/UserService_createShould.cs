using Xunit;

namespace User.UnitTests.Services;

public class UserServices_createShould
{
    [Fact]
    public void Create_ReturnsSuccessfully()
    {
        var userService = new UserCreateUseCase(
            new MockUserRepository(),
            new MockEncrypterRepository()
        );

        var actual = userService.execute(
            new(email: "example_4@example.com", password: "example_password", username: "example_4")
        );

        Assert.NotNull(actual.right);

        Assert.Equal("example_4@example.com", actual.right!.email);
        Assert.Equal("example_4", actual.right!.username);

        Assert.Equal("passwordHash", actual.right!.password);
        Assert.Null(actual.right?.id);
    }

    [Fact]
    public void Create_ReturnAlreadyExists()
    {
        var userService = new UserCreateUseCase(
            new MockUserRepository(),
            new MockEncrypterRepository()
        );

        var actual = userService.execute(new("username_2", "email_2@example.com", "password"));

        Assert.NotNull(actual.left);

        Assert.Equal("AlreadyExistsError", actual.left!.name);
        Assert.Equal("email already exists", actual.left!.error);
    }
}
