using Microsoft.EntityFrameworkCore;
using aspnet.Protocols;
using aspnet.Models;

namespace aspnet.Implements;

public class SqLiteUserImplement : RepositoryUser
{
    private DatabaseContext db { get; }
    private DbSet<UserModel> Users { get; }

    public SqLiteUserImplement()
    {
        this.db = new DatabaseContext();
        this.Users = this.db.Users;
    }

    public List<UserModel> getAll()
    {
        return this.Users.Include(u => u.posts).ToList();
    }

    public UserModel add(UserModel data)
    {
        this.Users.Add(data);
        this.db.SaveChanges();
        return data;
    }

    public List<UserModel> find(UserFindRequestDTO data)
    {
        return this.Users
            .Where(item => item.username == data.name || item.email == data.email)
            .Include(b => b.posts)
            .ToList();
    }

    public UserModel? findByEmail(string email)
    {
        return this.Users.IgnoreAutoIncludes().SingleOrDefault(user => user.email == email);
    }

    public UserModel? findById(Guid id)
    {
        return this.Users.IgnoreAutoIncludes().FirstOrDefault(user => user.id == id);
    }
}
