namespace aspnet.Api.Core.Protocols;

public abstract class ControllerHandle
{
    public abstract Response handle();
}

public abstract class ControllerHandle<Req>
{
    public abstract Response handle(Req data);
}

public class ControllerError
{
    public String error { get; set; } = "Error";
    public String message { get; set; } = "Unexpected Error";
    public dynamic? data { get; set; } = null;
}

public class Response
{
    public dynamic? value { get; set; } = null;
    public int statusCode { get; set; }
}
