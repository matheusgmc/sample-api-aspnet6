using Microsoft.EntityFrameworkCore;

using aspnet.Api.Core.Entities;
using aspnet.Api.Core.Protocols.Repositories;

namespace aspnet.Api.Infra.Database.Repositories;

public class SqLiteUserRepository : IUserRepository
{
    private readonly DbSet<UserEntity> Users;
    private readonly DatabaseContext db;

    public SqLiteUserRepository()
    {
        this.db = new DatabaseContext();
        if (this.db.Users == null)
            throw new Exception("user database not implemented");
        this.Users = this.db.Users;
    }

    public override List<UserEntity> getAll()
    {
        return this.Users.ToList();
    }

    public override UserEntity add(UserEntity data)
    {
        this.Users.Add(data);
        this.db.SaveChanges();
        return data;
    }

    public override List<UserEntity> find(IUserRepositoryFindData data)
    {
        return this.Users
            .Where(user => user.username == data.username)
            .Include(b => b.posts)
            .ToList();
    }

    public override UserEntity? findByEmail(string email)
    {
        return this.Users.SingleOrDefault(user => user.email == email);
    }

    public override UserEntity? findById(Guid id)
    {
        return this.Users.FirstOrDefault(user => user.id == id);
    }
}
