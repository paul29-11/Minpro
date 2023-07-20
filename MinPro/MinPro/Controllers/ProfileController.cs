using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Services;
using MinPro.viewmodels;
using xpos319.viewmodels;

namespace MinPro.Controllers
{
    public class ProfileController : Controller


    {
        private ProfileService profileService;
        private int IdUser = 1;

        public ProfileController(ProfileService _profileService)
        {
            this.profileService = _profileService;
        }
        public async Task<IActionResult> Index()
        {
            List<VMTblProfile> profiles = await profileService.GetAllData();
            return View(profiles);
        }


        public IActionResult Pasien()
        {
            return View();
        }


        public async Task<IActionResult> Edit(int id)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            List<VMTblProfile> listCategory = await profileService.GetAllData();
            ViewBag.ListCategory = listCategory;
            return PartialView(data);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(VMTblProfile dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await profileService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

         public async Task<IActionResult> EditM(int id)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            List<VMTblProfile> listCategory = await profileService.GetAllData();
            ViewBag.ListCategory = listCategory;
            return PartialView(data);
        }

        [HttpPost()]
        public async Task<IActionResult> EditM(VMTblProfile dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await profileService.EditM(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }
        public async Task<IActionResult> EditP(int id)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            List<VMTblProfile> listCategory = await profileService.GetAllData();
            ViewBag.ListCategory = listCategory;
            return PartialView(data);
        }

        [HttpPost()]
        public async Task<IActionResult> EditP(VMTblProfile dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await profileService.EditP(dataParam);
            
            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }
    }
}
