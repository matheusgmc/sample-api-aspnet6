using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using aspnet.Api.Core.Protocols.Repositories;

namespace aspnet.Api.Infra.AuthToken;

public class AuthTokenRepository : IAuthTokenRepository
{
    private JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
    private byte[] secret_key = Encoding.ASCII.GetBytes("super_secret_key"); // this secret_key must be in the environment variables

    public override string createAuthToken(string user_id)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user_id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(this.secret_key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = this.TokenHandler.CreateToken(tokenDescriptor);

        return this.TokenHandler.WriteToken(token);
    }

    public override string decodedAuthToken(string access_token)
    {
        this.TokenHandler.ValidateToken(
            access_token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(this.secret_key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            },
            out SecurityToken validatedToken
        );

        var jwtToken = (JwtSecurityToken)validatedToken;

        var user_id = jwtToken.Claims.First(x => x.Type == "id").Value;
        return user_id;
    }
}
