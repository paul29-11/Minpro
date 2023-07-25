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



                select new VMDoctor
                {
                    //Id = c.Id,
                    Id = d.Id,
                    NameDoctor = b.Fullname,
                    NameSpecialis = s.Name,
                    //NameSpecialis = s.Name,
                    ImagePath = b.ImagePath,

                    IsDelete = d.IsDelete
                }).ToList();
            return data;
        }

        [HttpGet("GetById/{id}")]
        public VMDoctor GetById(int id)
        {
            VMDoctor data = (from c in db.TCurrentDoctorSpecializations
                             join d in db.MDoctors on c.DoctorId equals d.Id
                             join b in db.MBiodata on d.BiodataId equals b.Id

                             join s in db.MSpecializations on c.SpecializationId equals s.Id
                             


                             where c.IsDelete == false && d.Id == id
                            select new VMDoctor
                            {
                                Id = d.Id,
                                NameDoctor = b.Fullname,
                                ImagePath = b.ImagePath,
                                NameSpecialis = s.Name,


                                IsDelete = d.IsDelete
                            }).FirstOrDefault()!;
            return data;
        }

        [HttpGet("GetPendidikan")]
        public List<VMDoctor> GetPendidikan()
        {
            List<VMDoctor> data1 = (from p in db.MDoctorEducations
                                    join d in db.MDoctors on p.DoctorId equals d.Id
                                    where p.IsDelete == false && p.DoctorId == 3
                                    select new VMDoctor
                                    {
                                        //Id = c.Id,
                                        Id = d.Id,
                                        InstitutionName = p.InstitutionName,
                                        Major = p.Major,
                                        StartYear = p.StartYear,

                                        IsDelete = d.IsDelete
                                    }).ToList();
            return data1;
                              
        }

        [HttpGet("GetHarga/{id}")]
        public VMDoctor GetHarga(int id)
        {
            VMDoctor data1 = (from price in db.TDoctorOfficeTreatmentPrices
                             join toffice in db.TDoctorOfficeTreatments on price.DoctorOfficeTreatmentId equals toffice.Id

                             join office in db.TDoctorOffices on toffice.DoctorOfficeId equals office.Id
                             join doctor in db.MDoctors on office.DoctorId equals doctor.Id

                             where price.IsDelete == false && office.DoctorId == id
                             select new VMDoctor
                             {
                                 Id = doctor.Id,
                                 Price = price.Price,

                                 IsDelete = price.IsDelete
                             }).FirstOrDefault()!;
            return data1;   
        }

            [HttpGet("GetRiwayatPraktek")]
        public List<VMDoctor> GetRiwayatPraktek()
        {
            List<VMDoctor> data1 = (from dot in db.TDoctorOfficeTreatments
                                    join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                                    join dof in db.TDoctorOffices on dot.DoctorOfficeId equals dof.Id
                                    join d in db.MDoctors on dt.DoctorId equals d.Id
                                    join ds in db.MDoctors on dof.DoctorId equals ds.Id
                                    join mf in db.MMedicalFacilities on dof.MedicalFacilityId equals mf.Id
                                    join l in db.MLocations on mf.LocationId equals l.Id
                                    where dot.IsDelete == false && dt.DoctorId == 3
                                    select new VMDoctor
                                    {
                                        //Id = c.Id,
                                        Id = d.Id,
                                        NameLokasi = l.Name,
                                        NameSpecialis = dof.Specialization,
                                        StartTahun = dof.StartDate,
                                        EndTahun = dof.EndDate,

                                        IsDelete = d.IsDelete
                                    }).ToList();
            return data1;

        }

        [HttpGet("GetTindakanMedis")]
        public List<VMDoctor> GetTindakanMedis()
        {
            List<VMDoctor> data1 = (from t in db.TDoctorTreatments
                                    join d in db.MDoctors on t.DoctorId equals d.Id
                                    where t.IsDelete == false && t.DoctorId == 3
                                    select new VMDoctor
                                    {
                                        //Id = c.Id,
                                        Id = d.Id,
                                        NameTindakan = t.Name,

                                        IsDelete = d.IsDelete
                                    }).ToList();
            return data1;

        }


    }
}
