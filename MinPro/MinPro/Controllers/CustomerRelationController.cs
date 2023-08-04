using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Services;
using MinPro.viewmodels;

namespace MinPro.Controllers
{
    public class CustomerRelationController : Controller
    {

        private CustomerRelationService customerRelationService;
        private int IdUser = 1;

        public CustomerRelationController(CustomerRelationService _customerRelationService)
        {
            this.customerRelationService = _customerRelationService;
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


            List<MCustomerRelation> data = await customerRelationService.GetAllData();
            if (!string.IsNullOrEmpty(searchString))
            {

                data = data.Where(a => a.Name.ToLower().Contains(searchString.ToLower())
                || a.Name.ToLower().ToString().Contains(searchString.ToLower())

                ).ToList();
            }
            return View(PaginatedList<MCustomerRelation>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            MCustomerRelation data = new MCustomerRelation();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MCustomerRelation dataParam)
        {
            dataParam.CreatedBy = IdUser;

            VMResponse respon = await customerRelationService.Create(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }
        public async Task<JsonResult> CheckNameIsExist(string name, int id)
        {
            bool isExis = await customerRelationService.CheckNameById(name, id);
            return Json(isExis);
        }

        public async Task<IActionResult> Edit(int id)
        {
            MCustomerRelation data = await customerRelationService.GetDataById(id);
            return PartialView(data);
        }
        [HttpPost()]
        public async Task<IActionResult> Edit(MCustomerRelation dataParam)
        {
            dataParam.ModifiedBy = IdUser;

            VMResponse respon = await customerRelationService.Edit(dataParam);

            if (respon.Success)
            {
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            MCustomerRelation data = await customerRelationService.GetDataById(id);
            return PartialView(data);
        }
        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = IdUser;
            VMResponse respon = await customerRelationService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }
    }
}
