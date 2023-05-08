using Microsoft.AspNetCore.Mvc;

namespace aspnet.Api.Controllers;

[ApiController]
[Route("/")]
public class RootController : ControllerBase
{
    [HttpGet()]
    public Object index()
    {
        return new { message = "Hello World" };
    }
}
