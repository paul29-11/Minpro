using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Service;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class SpesialisasiController : Controller
    {
        private SpesialisasiService spesialisasiService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private int IdUser = 1;

        public SpesialisasiController(SpesialisasiService _spesialisasiService)
        {
            this.spesialisasiService = _spesialisasiService;
        }
        public async Task<IActionResult> Index(string sortOrder,
                                               string searchString,
                                               string currentFilter,
                                               int? pageNumber,
                                               int? pageSize)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSort = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<MSpecialization> data = await spesialisasiService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.Name.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Name).ToList();
                    break;
            }
            return View(PaginatedList<MSpecialization>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));

        }

        public async Task<JsonResult> CheckByName(string name, int id)
        {
            bool isExis = await spesialisasiService.CheckByName(name, id);
            return Json(isExis);
        }

        public async Task<IActionResult> Create()
        {
            MSpecialization data = new MSpecialization();

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MSpecialization dataParam)
        {

            VMResponse respon = await spesialisasiService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
                //return RedirectToAction("index");
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            MSpecialization data = await spesialisasiService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost] 
        public async Task<IActionResult> Edit(MSpecialization dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await spesialisasiService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MSpecialization data = await spesialisasiService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            VMResponse respon = await spesialisasiService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }
    }
}
