using Microsoft.AspNetCore.Mvc;
using MinPro.Service;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorService doctorService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DoctorController(DoctorService _doctorService)
        {
            this.doctorService = _doctorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DetailDoctor(int id)
        {
            VMDoctor data = await doctorService.GetById(3);
            List<VMDoctor> listTinMed = await doctorService.GetTindakanMedis();
            ViewBag.listTinMed = listTinMed;
            List<VMDoctor> listEdu = await doctorService.GetPendidikan();
            ViewBag.listEdu = listEdu;
            List<VMDoctor> listR = await doctorService.GetRiwayatPraktek();
            ViewBag.listR = listR;
            return View(data);
        }
    }
}
