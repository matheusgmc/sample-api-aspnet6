using aspnet.Api.Core.Protocols.Repositories;
using aspnet.Api.Core.Entities;
using System.Collections.Generic;
using System;

class MockUserRepository : IUserRepository
{
    private List<UserEntity> list = new List<UserEntity>();

    public MockUserRepository()
    {
        this.list.Add(new UserEntity("username_1", "email_1@example.com", "password", Guid.Empty));
        this.list.Add(new UserEntity("username_3", "email_3@example.com", "password", Guid.Empty));
    }

    public override List<UserEntity> getAll()
    {
        return this.list;
    }

    public override UserEntity? findById(Guid id)
    {
        throw new NotImplementedException();
    }

    public override List<UserEntity> find(IUserRepositoryFindData data)
    {
        return list.FindAll(item => item.username == data.username);
    }

    public override UserEntity? findByEmail(string email)
    {
        if (email == "email_2@example.com")
        {
            return new UserEntity("username_2", "email_2@example.com", "password", Guid.Empty);
        }
        return null;
    }

    public override UserEntity add(UserEntity data)
    {
        return data;
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
