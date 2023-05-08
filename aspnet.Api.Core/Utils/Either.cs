namespace aspnet.Api.Core.Utils;

public class Either<TLeft, TRight>
{
    public readonly TLeft? left;
    public readonly TRight? right;

    public Either(TLeft left)
    {
        this.left = left;
    }

    public Either(TRight right)
    {
        this.right = right;
    }
}
