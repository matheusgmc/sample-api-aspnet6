using aspnet.Utils;

namespace aspnet.Protocols;

public interface UseCaseHandle<Result, Props>
{
    public abstract Either<UseCaseError, Result> execute(Props props);
}

public interface UseCaseHandle<Result>
{
    public abstract Either<UseCaseError, Result> execute();
}

public class UseCaseError
{
    public string error { get; set; }

    public UseCaseError(string error)
    {
        this.error = error;
    }
}
