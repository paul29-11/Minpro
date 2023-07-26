using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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
                    Fullname = b.Fullname,
                    SpecializationName = s.Name,
                    //NameSpecialis = s.Name,
                    ImagePath = b.ImagePath,

                    IsDelete = d.IsDelete
                }).ToList();
            return data;
        }


        [HttpGet("GetById/{id}")]
        public VMDoctor GetById(int id)
        {
            
            VMDoctor data = (
                                      from CurentDoctorSpesialisasi in db.TCurrentDoctorSpecializations
                                      join Doctor in db.MDoctors on CurentDoctorSpesialisasi.DoctorId equals Doctor.Id
                                      join Biodata in db.MBiodata on Doctor.BiodataId equals Biodata.Id
                                      join Spesialisasi in db.MSpecializations on CurentDoctorSpesialisasi.SpecializationId equals Spesialisasi.Id
                                      where CurentDoctorSpesialisasi.IsDelete == false && Doctor.IsDelete == false && Biodata.IsDelete == false && Spesialisasi.IsDelete == false
                                      && CurentDoctorSpesialisasi.DoctorId == id
                                      //from Doctor in db.MDoctors
                                      //join Biodata in db.MBiodata on Doctor.BiodataId equals Biodata.Id
                                      //join DoctorOffice in db.TDoctorOffices on Doctor.Id equals DoctorOffice.DoctorId
                                      //join MedicalFacility in db.MMedicalFacilities on DoctorOffice.MedicalFacilityId equals MedicalFacility.Id
                                      //join DoctorTreatment in db.TDoctorTreatments on DoctorOffice.Id equals DoctorTreatment.DoctorId
                                      //join DoctorTreatmentPrice in db.TDoctorOfficeTreatmentPrices on DoctorTreatment.Id equals DoctorTreatmentPrice.DoctorOfficeTreatmentId


                                      //where Doctor.IsDelete == false && DoctorOffice.IsDelete == false && MedicalFacility.IsDelete == false
                                      //&& Doctor.Id == id
                                      select new VMDoctor
                                      {
                                          DoctorId = Doctor.Id,
                                          BiodataId = Biodata.Id,
                                          Str = Doctor.Str,

                                          SpecializationName = Spesialisasi.Name,

                                          Fullname = Biodata.Fullname,
                                          MobilePhone = Biodata.MobilePhone,
                                          ImagePath = Biodata.ImagePath,
                                          //Price = DoctorTreatmentPrice.Price,

                                          
                                          Treatment = (from DoctorTreatment in db.TDoctorTreatments
                                                       join Doctor in db.MDoctors on DoctorTreatment.DoctorId equals Doctor.Id
                                                       where DoctorTreatment.IsDelete == false && Doctor.IsDelete == false
                                                       && DoctorTreatment.DoctorId == id
                                                       select new VMDoctorTreatment
                                                       {

                                                           DoctorId = Doctor.Id,
                                                           Name = DoctorTreatment.Name,

                                                           CreatedBy = DoctorTreatment.CreatedBy,
                                                           CreatedOn = DoctorTreatment.CreatedOn

                                                       }).ToList(),

                                          Location = (from Doctor in db.MDoctors
                                                      join Biodata in db.MBiodata on Doctor.BiodataId equals Biodata.Id
                                                      join DoctorOffice in db.TDoctorOffices on Doctor.Id equals DoctorOffice.DoctorId
                                                      join MedicalFacility in db.MMedicalFacilities on DoctorOffice.MedicalFacilityId equals MedicalFacility.Id
                                                      join Location in db.MLocations on MedicalFacility.LocationId equals Location.Id
                                                      join CurrentDoctorSpesialisasi in db.TCurrentDoctorSpecializations on Doctor.Id equals CurentDoctorSpesialisasi.Id
                                                      join Spesialiasai in db.MSpecializations on CurentDoctorSpesialisasi.SpecializationId equals Spesialiasai.Id
                                                             select new VMLocation
                                                             {
                                                                 DoctorId = DoctorOffice.DoctorId,
                                                                 MedicalFacilityId = MedicalFacility.Id,
                                                                 MedicalFacilityName = MedicalFacility.Name,

                                                                 Specialization = DoctorOffice.Specialization,
                                                                 Location = Location.Name,
                                                                 StartDate = DoctorOffice.StartDate,
                                                                 EndDate = DoctorOffice.EndDate,

                                                                 CreatedBy = DoctorOffice.CreatedBy,
                                                                 CreatedOn = DoctorOffice.CreatedOn
                                                             }).ToList(),

                                          LocationHistory = (from DoctorOffice in db.TDoctorOffices
                                                             join MedicalFacility in db.MMedicalFacilities on DoctorOffice.MedicalFacilityId equals MedicalFacility.Id
                                                             join MedicalFacilityCategory in db.MMedicalFacilityCategories on MedicalFacility.MedicalFacilityCategoryId equals MedicalFacilityCategory.Id
                                                             join Location in db.MLocations on MedicalFacility.LocationId equals Location.Id
                                                             where DoctorOffice.IsDelete == false && MedicalFacility.IsDelete == false && MedicalFacilityCategory.IsDelete == false && Location.IsDelete == false
                                                             && DoctorOffice.DoctorId == id
                                                             select new VMLocationHistory
                                                             {
                                                                 DoctorId = DoctorOffice.DoctorId,
                                                                 MedicalFacilityId = MedicalFacility.Id,
                                                                 MedicalFacilityName = MedicalFacility.Name,
                                                                 Specialization = DoctorOffice.Specialization,
                                                                 Location = Location.Name,
                                                                 StartDate = DoctorOffice.StartDate,
                                                                 EndDate = DoctorOffice.EndDate,

                                                                 CreatedBy = DoctorOffice.CreatedBy,
                                                                 CreatedOn = DoctorOffice.CreatedOn
                                                             }).ToList(),

                                          DoctorEducation = (from Doctor in db.MDoctors
                                                             join Biodata in db.MBiodata on Doctor.BiodataId equals Biodata.Id
                                                             join DoctorEducation in db.MDoctorEducations on Doctor.Id equals DoctorEducation.DoctorId
                                                             join EducationLevel in db.MEducationLevels on DoctorEducation.EducationLevelId equals EducationLevel.Id
                                                             where Doctor.IsDelete == false && Biodata.IsDelete == false && DoctorEducation.IsDelete == false && EducationLevel.IsDelete == false
                                                             && DoctorEducation.DoctorId == id
                                                             select new VMDoctorEducation
                                                             {

                                                                 DoctorId = Doctor.Id,
                                                                 EducationLevelId = EducationLevel.Id,
                                                                 NameLevel = EducationLevel.Name,
                                                                 InstitutionName = DoctorEducation.InstitutionName,
                                                                 Major = DoctorEducation.Major,
                                                                 StartYear = DoctorEducation.StartYear,
                                                                 EndYear = DoctorEducation.EndYear,

                                                                 CreatedBy = DoctorEducation.CreatedBy,
                                                                 CreatedOn = DoctorEducation.CreatedOn

                                                             }).ToList()


                                      }).FirstOrDefault()!;

            return data;
        }

        //[HttpGet("GetPendidikan")]
        //public List<VMDoctor> GetPendidikan()
        //{
        //    List<VMDoctor> data1 = (from p in db.MDoctorEducations
        //                            join d in db.MDoctors on p.DoctorId equals d.Id
        //                            where p.IsDelete == false && p.DoctorId == 3
        //                            select new VMDoctor
        //                            {
        //                                //Id = c.Id,
        //                                DoctorId = d.Id,
        //                                InstitutionName = p.InstitutionName,
        //                                Major = p.Major,
        //                                StartYear = p.StartYear,

        //                                IsDelete = d.IsDelete
        //                            }).ToList();
        //    return data1;

        //}

        //[HttpGet("GetHarga/{id}")]
        //public VMDoctor GetHarga(int id)
        //{
        //    VMDoctor data1 = (from price in db.TDoctorOfficeTreatmentPrices
        //                      join toffice in db.TDoctorOfficeTreatments on price.DoctorOfficeTreatmentId equals toffice.Id

        //                      join office in db.TDoctorOffices on toffice.DoctorOfficeId equals office.Id
        //                      join doctor in db.MDoctors on office.DoctorId equals doctor.Id

        //                      where price.IsDelete == false && office.DoctorId == id
        //                      select new VMDoctor
        //                      {
        //                          DoctorId = doctor.Id,
        //                          Price = price.Price,

        //                          IsDelete = price.IsDelete
        //                      }).FirstOrDefault()!;
        //    return data1;
        //}

        //    [HttpGet("GetRiwayatPraktek")]
        //public List<VMDoctor> GetRiwayatPraktek()
        //{
        //    List<VMDoctor> data1 = (from dot in db.TDoctorOfficeTreatments
        //                            join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
        //                            join dof in db.TDoctorOffices on dot.DoctorOfficeId equals dof.Id
        //                            join d in db.MDoctors on dt.DoctorId equals d.Id
        //                            join ds in db.MDoctors on dof.DoctorId equals ds.Id
        //                            join mf in db.MMedicalFacilities on dof.MedicalFacilityId equals mf.Id
        //                            join l in db.MLocations on mf.LocationId equals l.Id
        //                            where dot.IsDelete == false && dt.DoctorId == 3
        //                            select new VMDoctor
        //                            {
        //                                //Id = c.Id,
        //                                Id = d.Id,
        //                                NameLokasi = l.Name,
        //                                NameSpecialis = dof.Specialization,
        //                                StartTahun = dof.StartDate,
        //                                EndTahun = dof.EndDate,

        //                                IsDelete = d.IsDelete
        //                            }).ToList();
        //    return data1;

        //}

        //[HttpGet("GetTindakanMedis")]
        //public List<VMDoctor> GetTindakanMedis()
        //{
        //    List<VMDoctor> data1 = (from t in db.TDoctorTreatments
        //                            join d in db.MDoctors on t.DoctorId equals d.Id
        //                            where t.IsDelete == false && t.DoctorId == 3
        //                            select new VMDoctor
        //                            {
        //                                //Id = c.Id,
        //                                Id = d.Id,
        //                                NameTindakan = t.Name,

        //                                IsDelete = d.IsDelete
        //                            }).ToList();
        //    return data1;

        //}


    }
}
