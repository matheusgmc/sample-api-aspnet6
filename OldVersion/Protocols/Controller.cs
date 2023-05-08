namespace aspnet.Protocols;

public interface ControllerHandle
{
    public Response handle();
}

public interface ControllerHandle<Req>
{
    public Response handle(Req item);
}
