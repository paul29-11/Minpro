using Microsoft.AspNetCore.Mvc;

namespace MinPro.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetailDoctor()
        {
            return View();
        }
    }
}
