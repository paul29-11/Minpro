using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.Services;
using MinPro.viewmodels;
using System.Drawing;
using MinPro.Services;
using System.Globalization;

namespace MinPro.Controllers
{
    public class PasienController : Controller
    {
        private PasienService pasienService;

        private  CustomerRelationService customerRelationService;
        private  BloodGroupService bloodGroupService;


        private int IdUser = 1;

        public PasienController(PasienService _pasienService,CustomerRelationService _customerRelationService, BloodGroupService _bloodGroupService)
        {
            this.pasienService = _pasienService;
            this.customerRelationService = _customerRelationService;
            this.bloodGroupService  = _bloodGroupService;
        }
        //public async Task<IActionResult> index(int id)
        //{
        //    List<VMTblPasien> data = await pasienService.GetDataById(2);
        //    // MAsukkan Id Diatas
        //    return View(data);
        //}

        public async Task<IActionResult> Index(string sortOrder,
                                                    string searchString,
                                                    string currentFilter,
                                                    int? pageNumber,
                                                    int? pageSize)
        {
            ViewBag.Currentsort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.AgeSort = sortOrder == "Age" ? "age_desc" : "age";
            ViewBag.ChatCountSort = sortOrder == "Chat" ? "chat_desc" : "chat";
            ViewBag.AppointmentCountSort = sortOrder == "Appointment" ? "appointment_desc" : "appointment";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            List<VMTblPasien> data = await pasienService.GetDataByIdParent(1);
            if (!string.IsNullOrEmpty(searchString))
            {

                data = data.Where(a => a.Fullname.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        data = data.OrderByDescending(a => a.Fullname).ToList();
            //        break;
            //    case "age":
            //        data = data.OrderBy(a => a.Dob).ToList();
            //        break;
            //    case "age_desc":
            //        data = data.OrderByDescending(a => a.Dob).ToList();
            //        break;
            //    case "chat":
            //        data = data.OrderBy(a => a.ChatCount).ToList();
            //        break;
            //    case "chat_desc":
            //        data = data.OrderByDescending(a => a.ChatCount).ToList();
            //        break;
            //    case "appointment":
            //        data = data.OrderBy(a => a.AppointmentCount).ToList();
            //        break;
            //    case "appointment_desc":
            //        data = data.OrderByDescending(a => a.AppointmentCount).ToList();
            //        break;
            //    default:
            //        data = data.OrderBy(a => a.Fullname).ToList();
            //        break;
            //}
            switch (sortOrder)
            {
                case "Name":
                    data = data.OrderBy(a => a.Fullname).ToList();
                    break;
                case "Age":
                    data = data.OrderByDescending(a => a.Dob).ToList();
                    break;
                case "Chat":
                    data = data.OrderBy(a => a.ChatCount).ToList();
                    break;
                case "Appointment":
                    data = data.OrderBy(a => a.AppointmentCount).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.Fullname).ToList();
                    break;
            }

            return View(PaginatedList<VMTblPasien>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }


        public async Task<IActionResult> Create()
        {
            VMTblPasien data = new VMTblPasien();

            data.ParentBiodataId = 1;//code untuk parent id

            // Get customer relations and blood groups data from the services
            List<MCustomerRelation> customerRelations = await customerRelationService.GetAllData();
            List<MBloodGroup> bloodGroups = await bloodGroupService.GetAllData();

            ViewBag.CustomerRelations = customerRelations;
            ViewBag.BloodGroups = bloodGroups; // Set ViewBag.BloodGroups with the bloodGroups data

            return PartialView(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(VMTblPasien dataParam)
        {
            VMResponse respon = await pasienService.Create(dataParam);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMTblPasien data = await pasienService.GetDataById(id);

            // Get customer relations and blood groups data from the services
            List<MCustomerRelation> customerRelations = await customerRelationService.GetAllData();
            List<MBloodGroup> bloodGroups = await bloodGroupService.GetAllData();

            ViewBag.CustomerRelations = customerRelations;
            ViewBag.BloodGroups = bloodGroups; // Set ViewBag.BloodGroups with the bloodGroups data

            return PartialView(data);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(VMTblPasien dataParam)
        {

            VMResponse respon = await pasienService.Edit(dataParam);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }

            return View(dataParam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMTblPasien data = await pasienService.GetDataById(id);
            return PartialView(data);

        }

         public async Task<IActionResult> MultipleDelete(List<int>listId)
        {
            List<string>listName = new List<string>();
            foreach (int item in listId)
            {
               VMTblPasien data = await pasienService.GetDataById(item);
                listName.Add(data.Fullname);
            }
            //ViewBag.listId = listId;
            ViewBag.listName = listName;

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {

            VMResponse respon = await pasienService.Delete(id);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> SureMultipleDelete(List<int> listId)
        {
            int createBy = IdUser;
            VMResponse respon = await pasienService.MultipleDelete(listId);

            if (respon.Success)
            {
                //return RedirectToAction("Index");
                return Json(new { dataRespon = respon });
            }
            return RedirectToAction("Index");
        }

    }
}
