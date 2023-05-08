using aspnet.Protocols;
using Microsoft.AspNetCore.Identity;

namespace aspnet.Implements;

public class EncrypterImplement : RepositoryEncrypter
{
    private PasswordHasher<String> hasher = new PasswordHasher<String>();

    public override string hashPassword(String user_id, String password)
    {
        return this.hasher.HashPassword(user_id, password);
    }

    public override bool verify(String user_id, String password, String hash)
    {
        var passwordVerify = this.hasher.VerifyHashedPassword(user_id, hash, password);
        return passwordVerify == PasswordVerificationResult.Success;
    }
}
