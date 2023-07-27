using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Service;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class TindakanController : Controller
    {
        private readonly DoctorService doctorService;
        private readonly TindakanService tindakanService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TindakanController(DoctorService doctorService, TindakanService tindakanService)
        {
            this.doctorService = doctorService;
            this.tindakanService = tindakanService;
        }

        public async Task<IActionResult> Index(int id)
        {
            //List<VMDoctorTreatment> data = await tindakanService.GetTreatment(3);
            VMDoctor data = await doctorService.GetById(3);


            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            TDoctorTreatment data = new TDoctorTreatment();

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TDoctorTreatment dataParam)
        {

            VMResponse respon = await tindakanService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
                //return RedirectToAction("index");
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TDoctorTreatment data = await tindakanService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            VMResponse respon = await tindakanService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }

    }
}
