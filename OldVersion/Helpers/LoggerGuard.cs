using Microsoft.AspNetCore.Mvc.Filters;
using aspnet.Helpers;

public interface ILoggerGuard
{
    void Send(string message) { }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LoggerGuard : Attribute, IActionFilter
{
    public String Start { get; set; }
    public int EventId = LogEvents.Response;
    public String Finish { get; set; }

    public LoggerGuard(
        int EventId = LogEvents.Response,
        String Start = "Start Process",
        String Finish = "Finish Process"
    )
    {
        this.EventId = EventId;
        this.Start = Start;
        this.Finish = Finish;
    }

    public void OnActionExecuting(ActionExecutingContext ctx)
    {
        var httpContext = ctx.HttpContext;

        var ControllerName = ctx.Controller.GetType().Name;

        var logger_factory = httpContext.RequestServices.GetService<ILoggerFactory>();

        if (logger_factory == null)
            return;

        var logger = logger_factory.CreateLogger($"{ControllerName}({httpContext.Request.Path})");

        logger.LogInformation(
            this.EventId,
            $"{this.Start}\nBody:{httpContext.Request.Body.Dump()}\nQuery:{httpContext.Request.Query.Dump()}"
        );
    }

    public void OnActionExecuted(ActionExecutedContext ctx)
    {
        var httpContext = ctx.HttpContext;

        var ControllerName = ctx.Controller.GetType().Name;
        var logger_factory = httpContext.RequestServices.GetService<ILoggerFactory>();

        if (logger_factory == null)
            return;

        var logger = logger_factory.CreateLogger($"{ControllerName}({httpContext.Request.Path})");

        logger.LogInformation(
            this.EventId,
            $"{this.Finish}\nStatusCode:{httpContext.Response.StatusCode}"
        );
    }
}
