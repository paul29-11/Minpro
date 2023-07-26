using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.Service;
using MinPro.viewmodels;
using System.Drawing;

namespace MinPro.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        //private RoleService roleService;
        VMResponse respon = new VMResponse();

        public AuthController(AuthService _authService)
        {
            this.authService = _authService;
        }
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> LoginSubmit(string email)
        {
            VMUser user = await authService.CheckLogin(email);
            if (user != null)
            {
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetInt32("IdRole", user.IdRole == null ? 0 : Convert.ToInt32(user.IdRole));
            }
            else
            {
                respon.Success = false;
                respon.Message = $"Oops, {email} not found pr password is wrong, please check it !";
            }
            return Json(new { dataRespon = respon });
        }


    }
}
