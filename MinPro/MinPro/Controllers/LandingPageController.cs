using Microsoft.AspNetCore.Mvc;
using MinPro.Service;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly FacilityService facilityService;
        private readonly DoctorService doctorService;
        private AuthService authService;
        private readonly IWebHostEnvironment webHostEnvironment;
       

        public LandingPageController(FacilityService _facilityService, DoctorService _doctorService, AuthService _authService)
        {
            this.facilityService = _facilityService;
            this.doctorService = _doctorService;
            this.authService = _authService;
        }
        public async Task<IActionResult> Index()
        {
            
            List<VMMedicalFacility> listFacility = await facilityService.GetAllData();
            ViewBag.listFacility = listFacility;
            List<VMDoctor> listDoctor = await doctorService.GetAllData();
            ViewBag.listDoctor = listDoctor;

            return View("Index", "_LayoutLandingPage");
        }

        public IActionResult Tentang()
        {
            return View();
        }

        public async Task<IActionResult> Doktor()
        {
            List<VMDoctor> listDoctor = await doctorService.GetAllData();
            ViewBag.listDoctor = listDoctor;

            return View();
        }

        public IActionResult Login()
        {
            return PartialView();
        }
        public IActionResult Daftar()
        {
            return PartialView();
        }

        public IActionResult LoginCoba()
        {
            return PartialView();
        }

        

    }
}
