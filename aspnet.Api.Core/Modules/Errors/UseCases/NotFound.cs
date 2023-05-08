using aspnet.Api.Core.Protocols;

namespace aspnet.Api.Core.Modules.Errors.UseCases;

public class NotFoundError : UseCaseError
{
    public override String error { get; set; }
    public override String name { get; set; }

    public NotFoundError(String error)
    {
        this.error = error;
        this.name = this.GetType().Name;
    }
}
