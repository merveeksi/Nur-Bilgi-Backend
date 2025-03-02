using Microsoft.AspNetCore.Mvc;

namespace NurBilgi.WebApi.Controllers;

public class AiChatMessage : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}