using aspnet.Api.Core.Protocols;

namespace aspnet.Api.Core.Modules.Errors.UseCases;

public class AuthInvalidError : UseCaseError
{
    public override String error { get; set; }
    public override String name { get; set; }

    public AuthInvalidError(String error = "email or name is invalid")
    {
        this.error = error;
        this.name = this.GetType().Name;
    }
}
