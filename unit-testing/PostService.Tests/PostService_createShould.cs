using Xunit;
using System;

namespace Post.UnitTests.Services;

public class PostServices_createShould
{
    private Guid user_id = Guid.NewGuid();
    private PostCreateUseCase postService;

    public PostServices_createShould()
    {
        var mockUserRepository = new MockUserRepository();
        var mockPostRepository = new MockPostRepository();

        mockUserRepository.add(
            new(
                username: "username_5",
                password: "passwordHash",
                email: "example_5@example.com",
                id: user_id
            )
        );

        this.postService = new PostCreateUseCase(mockUserRepository, mockPostRepository);
    }

    [Fact]
    public void ItShouldSuccessfullyCreate()
    {
        var actual = this.postService.execute(
            new(
                user_id: this.user_id,
                title: "example_title_3",
                description: "example_description_1"
            )
        );

        Assert.Null(actual.left);
        Assert.NotNull(actual.right);

        Assert.IsType<Guid>(actual.right!.id);

        Assert.Equal("example_title_3", actual.right!.title);
        Assert.Equal("example_description_1", actual.right!.description);
        Assert.Equal(this.user_id, actual.right!.user_id);
    }

    [Fact]
    public void ItShouldReturnTitleAlreadyExists()
    {
        var actual = this.postService.execute(
            new(
                user_id: this.user_id,
                title: "example_title_1",
                description: "example_description_1"
            )
        );

        Assert.Null(actual.right);
        Assert.NotNull(actual.left);

        Assert.Equal("there's already one post with this title", actual.left!.error);
        Assert.Equal("AlreadyExistsError", actual.left!.name);
    }

    [Fact]
    public void ItShouldReturnUserNotFound()
    {
        var actual = this.postService.execute(
            new(user_id: Guid.Empty, title: "example_title_5", description: "example_description")
        );

        Assert.Null(actual.right);
        Assert.NotNull(actual.left);

        Assert.Equal("user not found", actual.left!.error);
        Assert.Equal("NotFoundError", actual.left!.name);
    }
}
