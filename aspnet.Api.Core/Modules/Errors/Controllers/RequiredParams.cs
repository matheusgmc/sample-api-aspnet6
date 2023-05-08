using aspnet.Api.Core.Protocols;

namespace aspnet.Api.Core.Modules.Errors.Controllers;

public class RequiredParamsError : ControllerError
{
    public RequiredParamsError(dynamic? data = null)
    {
        this.data = data;
        this.message = "Some property are empty";
        this.error = "RequiredParamsError";
    }
}
