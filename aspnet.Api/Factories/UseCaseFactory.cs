using System.Reflection;

public class UseCaseFactory
{
    private BaseRepositories repositories;
    private PropertyInfo[] properties;

    public UseCaseFactory()
    {
        this.repositories = new BaseRepositories();
        this.properties = this.repositories.GetType().GetProperties();
    }

    public TUseCase Build<TUseCase>()
    {
        var useCaseType = typeof(TUseCase);
        var ctor = useCaseType.GetConstructors()[0];
        var args = new List<Object>();

        foreach (var param in ctor.GetParameters())
        {
            var property = properties.FirstOrDefault(
                e => e.PropertyType.Name == param.ParameterType.Name
            );

            if (property == null)
                throw new Exception($"dependency not found {param.ParameterType.Name}");

            var value = property.GetValue(this.repositories, null);
            if (value == null)
                throw new Exception($"value not found {param.ParameterType.Name}");

            args.Add(value);
        }

        TUseCase obj = (TUseCase)ctor.Invoke(args.ToArray());
        return obj;
    }
}
