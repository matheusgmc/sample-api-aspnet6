using aspnet.Api.Core.Protocols;

namespace aspnet.Api.Core.Modules.Errors.Controllers;

public class InvalidParamsError : ControllerError
{
    public InvalidParamsError(dynamic? data = null)
    {
        this.data = data;
        this.message = "Some property are invalid";
        this.error = "InvalidParamsError";
    }
}
