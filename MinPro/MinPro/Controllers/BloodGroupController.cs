using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Services;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class BloodGroupController : Controller
    {

        private BloodGroupService bloodGroupService;
        private int IdUser = 1;

        public BloodGroupController(BloodGroupService _bloodGroupService)
        {
            this.bloodGroupService = _bloodGroupService;
        }

        public async Task<IActionResult> Index(string sortOrder,
                                               string searchString,
                                               string currentFilter,
                                               int? pageNumber,
                                               int? pageSize)
        {
            ViewBag.Currentsort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<MBloodGroup> data = await bloodGroupService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Code.ToLower().Contains(searchString.ToLower())
                                    || (a.Description != null && a.Description.ToLower().Contains(searchString.ToLower()))).ToList();
            }

            return View(PaginatedList<MBloodGroup>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            MBloodGroup data = new MBloodGroup();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MBloodGroup dataParam)
        {
            dataParam.CreatedBy = IdUser;

            VMResponse respon = await bloodGroupService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }
        public async Task<JsonResult> CheckCodeIsExist(string code, int Id)
        {
            bool isExis = await bloodGroupService.CheckBloodTypeIsExist(code, Id);
            return Json(isExis);
        }

        public async Task<IActionResult> Edit(int id)
        {
            MBloodGroup data = await bloodGroupService.GetDataById(id);
            return PartialView(data);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(MBloodGroup dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await bloodGroupService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MBloodGroup data = await bloodGroupService.GetDataById(id);
            return PartialView(data);
        }
        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = IdUser;
            VMResponse respon = await bloodGroupService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }
    }
}
