using Microsoft.AspNetCore.Mvc;

namespace Contacts.Web.Controllers;

public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("Error")]
    public IActionResult Index()
    {
        return View();
    }
}