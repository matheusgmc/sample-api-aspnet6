using Xunit;

namespace User.UnitTests.Services;

public class UserServices_getAllShould
{
    [Fact]
    public void getAll_ReturnsUserListWith2Items()
    {
        var userService = new UserFindUseCase(new MockUserRepository());

        var actual = userService.execute(null);

        Assert.NotNull(actual.right);
        Assert.NotEmpty(actual.right);
        Assert.Equal(2, actual.right?.Capacity);
    }

    [Fact]
    public void getAll_InputIsUsername_ReturnsUserListWith1Items()
    {
        var userService = new UserFindUseCase(new MockUserRepository());

        var actual = userService.execute(new() { username = "username_1" });

        Assert.NotNull(actual.right);
        Assert.NotEmpty(actual.right);
        Assert.Equal(1, actual.right?.Capacity);
    }
}
