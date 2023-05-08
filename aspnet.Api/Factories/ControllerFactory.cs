public class ControllerFactory
{
    private UseCaseFactory useFactory;

    public ControllerFactory()
    {
        this.useFactory = new UseCaseFactory();
    }

    public TController Build<TController, TUseCase>()
    {
        var controllerType = typeof(TController);

        var ctor = controllerType.GetConstructors()[0];

        var args = new List<Object>();

        var useCase = this.useFactory.Build<TUseCase>();

        if (useCase == null)
            throw new Exception("use case is null");

        TController obj = (TController)ctor.Invoke(new object[] { useCase });
        return obj;
    }
}
