using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiDocterController : ControllerBase
    {
        private readonly DB_SpesificationContext db;

        public apiDocterController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMDoctor> GetAllData()
        {
            List<VMDoctor> data = (
                from c in db.TCurrentDoctorSpecializations
                join d in db.MDoctors on c.DoctorId equals d.Id
                join b in db.MBiodata on d.BiodataId equals b.Id
                join s in db.MSpecializations on c.SpecializationId equals s.Id
                where c.IsDelete == false
                //from c in db.TCurrentDoctorSpecializations
                //join d in db.MDoctors on c.DoctorId equals d.Id
                //join b in db.MBiodata on d.BiodataId equals b.Id
                //join s in db.MSpecializations on c.SpecializationId equals s.Id
                //join de in db.MDoctorEducations on d.BiodataId equals de.Id
                //join l in db.TDoctorOffices on c.DoctorId equals l.DoctorId
                //join sch in db.TDoctorOfficeSchedules on c.DoctorId equals sch.DoctorId

                where c.IsDelete == false
               
                    select new VMDoctor
                    { 
                        Id = c.Id,
                        NameDoctor = b.Fullname,
                        NameSpecialis = s.Name,
                        ImagePath = b.ImagePath,

                        IsDelete = c.IsDelete
                    }).ToList();
            return data;
        }
    }
}
