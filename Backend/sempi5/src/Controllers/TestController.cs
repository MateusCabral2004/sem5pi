using Microsoft.AspNetCore.Mvc;

namespace Sempi5.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string[] getAllTest()
    {
        string [] test = {"test1", "test2", "test3"};
        return test;
    }
}