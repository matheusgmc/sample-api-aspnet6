using aspnet.Protocols;

namespace aspnet.Helpers;

public class Ok : Response
{
    public Ok()
    {
        this.statusCode = 200;
    }
}

public class BadRequest : Response
{
    public BadRequest()
    {
        this.statusCode = 400;
    }
}

public class Created : Response
{
    public Created()
    {
        this.statusCode = 200;
    }
}
