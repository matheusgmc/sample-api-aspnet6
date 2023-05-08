using Xunit;
using System;

namespace Post.UnitTests.Services;

public class PostServices_updateShould
{
    private Guid user_id = Guid.NewGuid();
    private Guid post_id = Guid.NewGuid();
    private PostUpdateUseCase postService;

    public PostServices_updateShould()
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

        mockPostRepository.add(
            new(title: "example_title", description: "example_description", user_id)
            {
                id = post_id
            }
        );

        this.postService = new PostUpdateUseCase(mockPostRepository);
    }

    [Fact]
    public void ItShouldSuccessfullyUpdate()
    {
        var actual = this.postService.execute(
            new(post_id: this.post_id, user_id: this.user_id) { title = "example_title_update" }
        );

        Assert.Null(actual.left);
        Assert.NotNull(actual.right);

        Assert.IsType<Guid>(actual.right!.id);

        Assert.Equal("example_title_update", actual.right!.title);
        Assert.Equal("example_description", actual.right!.description);
        Assert.Equal(this.user_id, actual.right!.user_id);
    }

    [Fact]
    public void ItShouldReturnPostNotFound()
    {
        var actual = this.postService.execute(
            new(
                post_id: Guid.Empty,
                user_id: this.user_id,
                title: "example_title_1",
                description: "example_description_1"
            )
        );

        Assert.Null(actual.right);
        Assert.NotNull(actual.left);

        Assert.Equal("post not found", actual.left!.error);
        Assert.Equal("NotFoundError", actual.left!.name);
    }

    [Fact]
    public void ItShouldReturnUserNotBelong()
    {
        var actual = this.postService.execute(new(post_id: this.post_id, user_id: Guid.Empty));
        Assert.Null(actual.right);
        Assert.NotNull(actual.left);

        Assert.Equal("this post does not belong to the given user", actual.left!.error);
        Assert.Equal("NotBelongError", actual.left!.name);
    }
}
