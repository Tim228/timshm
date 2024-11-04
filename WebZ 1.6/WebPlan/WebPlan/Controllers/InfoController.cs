using Microsoft.AspNetCore.Mvc;

namespace WebPlan.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Privacy() => View();

        public IActionResult Contacts() => View();

        public IActionResult Cookie() => View();
    }
}
