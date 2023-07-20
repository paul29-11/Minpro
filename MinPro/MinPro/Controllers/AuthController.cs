using Microsoft.AspNetCore.Mvc;
using MinPro.Service;

namespace MinPro.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        //private RoleService roleService;

        public AuthController(AuthService _authService)
        {
            this.authService = _authService;
        }
        public IActionResult Login()
        {
            return PartialView();
        }
    }
}
