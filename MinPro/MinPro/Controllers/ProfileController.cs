using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Services;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class ProfileController : Controller


    {
        private ProfileService profileService;
        private int IdUser = 21;

        public ProfileController(ProfileService _profileService)
        {
            this.profileService = _profileService;
        }
        public async Task<IActionResult> Index()
        {
            VMTblProfile profiles = await profileService.GetDataById(21);
            return View(profiles);
        }

        public IActionResult Pasien()
        {
            return View();
        }

        public async Task<JsonResult> CheckPasswordIsExist(string password, int id)
        {
            bool isExis = await profileService.CheckByPassword(password, id);
            return Json(isExis);
        }

        public async Task<JsonResult> CheckEmailIsExist(string email, int id)
        {
            bool isExis = await profileService.CheckByEmail(email, id);
            return Json(isExis);
        }

        //public async Task<JsonResult> CheckOTP(string token, int id)
        //{
        //    bool isExis = await profileService.CheckOTP(token, id);
        //    return Json(isExis);
        //}

        public async Task<IActionResult> CheckOTP(string token, int id)
        {
            VMResponse respon = await profileService.CheckOTP(token, id);
            //if (respon.Success)
            //{
                return Json(new { dataRespon = respon });
            //}

            //return PartialView(respon);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
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
          
            return PartialView(data);
        }

        //[HttpPost]
        //public async Task<IActionResult> OtpEditM(int id, string email)
        //{
        //    HttpContext.Session.SetString("EmailBaru", email);

        //    VMTblProfile data = await profileService.GetDataById(id);
        //    return PartialView(data);
        //}

        //[HttpPost]
        //public async Task<IActionResult> OtpEditM(MUser dataParam.)
        //{
        //    HttpContext.Session.SetString("EmailBaru", email);
        //    dataParam.Email = HttpContext.Session.GetString("EmailBaru");
        //    VMResponse respon = await profileService.SendOTP(dataParam);

        //    if (respon.Success)
        //    {
        //        return Json(new { dataRespon = respon });
        //    }

        //    return View(dataParam);
        //}

        [HttpPost]
        public async Task<IActionResult> OtpEditM(MUser dataParam)
        {
            string email = dataParam.Email; 
            HttpContext.Session.SetString("EmailBaru", email);

            VMResponse respon = await profileService.SendOTP(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitMail(string email, int id)
        {
            MUser dataParam = new MUser();
            // string email = dataParam.Email;
            //HttpContext.Session.SetString("EmailBaru", email);
            dataParam.Email = HttpContext.Session.GetString("EmailBaru");
            dataParam.Id = id;
            VMResponse respon = await profileService.SubmitMail(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<IActionResult> OtpEditM(int id,string email)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            return PartialView(data);
        }




        public async Task<IActionResult> EditP(int id)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            return PartialView(data);
        }

        public async Task<IActionResult> SureEditP(int id)
        {
            VMTblProfile data = await profileService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureEditP(MUser dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await profileService.SureEditP(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

       
    }

}

