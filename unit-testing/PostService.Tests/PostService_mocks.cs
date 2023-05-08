using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Entities;
using System.Collections.Generic;
using System;

class MockUserRepository : IUserRepository
{
    private List<UserEntity> list = new List<UserEntity>();

    public MockUserRepository()
    {
        this.list.Add(
            new UserEntity("username_1", "email_1@example.com", "password", Guid.NewGuid())
        );
        this.list.Add(
            new UserEntity("username_3", "email_3@example.com", "password", Guid.NewGuid())
        );
    }

    public override List<UserEntity> getAll()
    {
        throw new NotImplementedException();
    }

    public override UserEntity? findById(Guid id)
    {
        return this.list.Find(post => post.id == id);
    }

    public override List<UserEntity> find(IUserRepositoryFindData data)
    {
        return list.FindAll(item => item.username == data.username);
    }

    public override UserEntity? findByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public override UserEntity add(UserEntity data)
    {
        this.list.Add(data);
        return data;
    }
}

class MockPostRepository : IPostRepository
{
    private List<PostEntity> list = new List<PostEntity>();

    public MockPostRepository()
    {
        this.list.Add(
            new PostEntity("example_title_1", "example_description_2", user_id: Guid.NewGuid())
        );
        this.list.Add(
            new PostEntity("example_title_2", "example_description_3", user_id: Guid.NewGuid())
        );
    }

    public override List<PostEntity> getAll()
    {
        throw new NotImplementedException();
    }

    public override PostEntity add(PostEntity data)
    {
        this.list.Add(data);
        return data;
    }

    public override PostEntity? findById(Guid id)
    {
        return this.list.Find(post => post.id == id);
    }

    public override PostEntity? update(PostEntity data)
    {
        var index = this.list.FindIndex(post => data.id == post.id);
        this.list[index].title = data.title;
        return data;
    }

    public override PostEntity? findByTitle(string title)
    {
        if (title == this.list[0].title)
        {
            return this.list[0];
        }
        return null;
    }
}

class MockEncrypterRepository : IEncrypterRepository
{
    public override string hashPassword(string user_id, string password)
    {
        return "passwordHash";
    }

    public override bool verify(string user_id, string password, string hash)
    {
        throw new NotImplementedException();
    }
}
