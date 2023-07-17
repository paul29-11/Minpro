using Microsoft.AspNetCore.Mvc;
using MinPro.Service;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly FacilityService facilityService;
        private readonly DoctorService doctorService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private int IdUser = 1;

        public LandingPageController(FacilityService _facilityService, DoctorService _doctorService)
        {
            this.facilityService = _facilityService;
            this.doctorService = _doctorService;
        }
        public async Task<IActionResult> Index()
        {
            
            List<VMMedicalFacility> listFacility = await facilityService.GetAllData();
            ViewBag.listFacility = listFacility;
            List<VMDoctor> listDoctor = await doctorService.GetAllData();
            ViewBag.listDoctor = listDoctor;

            return View();
        }

    }
}
