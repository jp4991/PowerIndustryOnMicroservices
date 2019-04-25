using Microsoft.AspNetCore.Mvc;

namespace Settlement.API.Controllers
{
	public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}