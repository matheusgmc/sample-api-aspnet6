using aspnet.Api.Core.Utils;

namespace aspnet.Api.Core.Protocols;

public abstract class UseCaseHandle<Result, Props>
{
    public abstract Either<UseCaseError, Result> execute(Props data);
}

public abstract class UseCaseHandle<Result>
{
    public abstract Either<UseCaseError, Result> execute();
}

public abstract class UseCaseError
{
    public abstract String error { get; set; }
    public abstract String name { get; set; }
}
