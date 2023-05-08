using Microsoft.EntityFrameworkCore;
using aspnet.Models;
using aspnet.Protocols;

namespace aspnet.Implements;

public class SqLitePostImplement : RepositoryPost
{
    private readonly DatabaseContext db;

    private readonly DbSet<PostModel> Posts;

    public SqLitePostImplement()
    {
        this.db = new DatabaseContext();
        this.Posts = this.db.Posts;
    }

    public override List<PostModel> getAll()
    {
        return this.Posts.ToList();
    }

    public override PostModel add(PostModel data)
    {
        var newPost = new PostModel(
            title: data.title,
            description: data.description,
            user_id: data.user_id
        );

        this.Posts.Add(newPost);
        this.db.SaveChanges();
        return newPost;
    }

    public override PostModel? findByTitle(string title)
    {
        return this.Posts.FirstOrDefault(post => post.title == title);
    }

    public override PostModel update(PostModel data)
    {
        this.db.ChangeTracker.DetectChanges();
        Console.WriteLine(this.db.ChangeTracker.DebugView.LongView);
        return data;
    }

    public override PostModel? findById(Guid id)
    {
        return this.Posts.FirstOrDefault(post => post.id == id);
        ;
    }
}
