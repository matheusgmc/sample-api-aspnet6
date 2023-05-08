namespace aspnet.Utils;

public class Either<TLeft, TRight>
{
    public readonly TLeft? left;
    public readonly TRight? right;
    public readonly bool isLeft;

    public Either(TLeft left)
    {
        this.left = left;
        this.isLeft = true;
    }

    public Either(TRight right)
    {
        this.right = right;
        this.isLeft = false;
    }
}
