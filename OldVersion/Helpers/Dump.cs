using System.Text.Json;

namespace aspnet.Helpers;

public static class ObjectExtensions
{
    public static string Dump(this object obj)
    {
        try
        {
            return JsonSerializer.Serialize(obj);
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
