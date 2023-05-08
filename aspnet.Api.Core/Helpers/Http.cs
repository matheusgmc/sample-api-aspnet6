using aspnet.Api.Core.Protocols;

namespace aspnet.Api.Core.Helpers;

public class Ok : Response
{
    public Ok(dynamic value)
    {
        this.value = value;
        this.statusCode = 200;
    }
}

public class BadRequest : Response
{
    public BadRequest(ControllerError value)
    {
        this.value = value;
        this.statusCode = 400;
    }

    public BadRequest(UseCaseError value)
    {
        this.value = value;
        this.statusCode = 400;
    }
}

public class Created : Response
{
    public Created(dynamic value)
    {
        this.value = value;
        this.statusCode = 200;
    }
}
