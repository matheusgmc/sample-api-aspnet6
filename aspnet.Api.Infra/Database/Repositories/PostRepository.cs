using Microsoft.EntityFrameworkCore;
using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols.Repositories;

namespace aspnet.Api.Infra.Database.Repositories;

public class SqLitePostRepository : IPostRepository
{
    private readonly DbSet<PostEntity> Posts;
    private readonly DatabaseContext db;

    public SqLitePostRepository()
    {
        this.db = new DatabaseContext();

        if (this.db.Posts == null)
            throw new Exception("post database not implement");

        this.Posts = this.db.Posts;
    }

    public override List<PostEntity> getAll()
    {
        return this.Posts.ToList();
    }

    public override PostEntity add(PostEntity data)
    {
        this.Posts.Add(data);
        this.db.SaveChanges();
        return data;
    }

    public override PostEntity? update(PostEntity data)
    {
        this.db.ChangeTracker.DetectChanges();

        Console.WriteLine(this.db.ChangeTracker.DebugView.LongView);
        return data;
    }

    public override PostEntity? findByTitle(string title)
    {
        return this.Posts.FirstOrDefault(post => post.title == title);
    }

    public override PostEntity? findById(Guid id)
    {
        return this.Posts.FirstOrDefault(post => post.id == id);
    }
}
