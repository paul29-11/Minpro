using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiFacilityController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        //private VMResponse respon = new VMResponse();
        //private int IdUser = 1;

        public apiFacilityController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMMedicalFacility> GetAllData()
        {
            List<VMMedicalFacility> data = (from facility in db.MMedicalFacilities
                                       //join fcategory in db.MMedicalFacilityCategories on facility.MedicalFacilityCategoryId equals fcategory.Id
                                       //join location in db.MLocations on facility.LocationId equals location.Id
                                       //where v.IsDelete == false
                                       where facility.IsDelete == false
                                       select new VMMedicalFacility
                                       {
                                           Id = facility.Id,
                                           Name = facility.Name,
                                           FullAddress = facility.FullAddress,
                                           Email = facility.Email,
                                           PhoneCode = facility.PhoneCode,
                                           Phone = facility.Phone,
                                           Fax = facility.Fax,

                                           MedicalFacilityCategoryId = facility.MedicalFacilityCategoryId,
                                           //NameFacilityCategory = fcategory.Name,

                                           LocationId = facility.LocationId,
                                           //NameLocation = location.Name,

                                           //CreateBy = p.CreateBy,
                                           //CreateDate = p.CreateDate,

                                           IsDelete = facility.IsDelete
                                       }).ToList();
            return data;
        }

    }
}
