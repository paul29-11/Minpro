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

        int currentYear = DateTime.Now.Year;

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
                                     
                                      select new VMDoctor
                                      {
                                          DoctorId = Doctor.Id,
                                          BiodataId = Biodata.Id,
                                          //Str = Doctor.Str,

                                          SpecializationName = Spesialisasi.Name,

                                          Fullname = Biodata.Fullname,
                                          //MobilePhone = Biodata.MobilePhone,
                                          ImagePath = Biodata.ImagePath,

                                          Chat = (from MedicalItem in db.MMedicalItems
                                                  join MedicalItemCategory in db.MMedicalItemCategories on MedicalItem.MedicalItemCategoryId equals MedicalItemCategory.Id
                                                  join MedicalItemSegmentation in db.MMedicalItemSegmentations on MedicalItem.MedicalItemSegmentationId equals MedicalItemSegmentation.Id

                                                  where MedicalItem.IsDelete == false
                                                  select new VMMedicalItem
                                                  {
                                                      PriceMin = MedicalItem.PriceMin,
                                                      PriceMax = MedicalItem.PriceMax
                                                  }).FirstOrDefault(),

                                          Treatment = (from DoctorTreatment in db.TDoctorTreatments
                                                       join Doctor in db.MDoctors on DoctorTreatment.DoctorId equals Doctor.Id

                                                       where DoctorTreatment.IsDelete == false && Doctor.IsDelete == false
                                                       && DoctorTreatment.DoctorId == id
                                                       select new VMDoctorTreatment
                                                       {
                                                           Id = DoctorTreatment.Id,
                                                           DoctorId = Doctor.Id,
                                                           Name = DoctorTreatment.Name

                                                       }).ToList(),

                                          Location = (from DoctorOfficeTreatments in db.TDoctorOfficeTreatments
                                                      join DoctorTreatment in db.TDoctorTreatments on DoctorOfficeTreatments.DoctorTreatmentId equals DoctorTreatment.Id
                                                      join DoctorOfficeTreatmentPrice in db.TDoctorOfficeTreatmentPrices on DoctorOfficeTreatments.Id equals DoctorOfficeTreatmentPrice.DoctorOfficeTreatmentId
                                                      join DoctorOffice in db.TDoctorOffices on DoctorOfficeTreatments.DoctorOfficeId equals DoctorOffice.Id
                                                      join Doctor in db.MDoctors on DoctorTreatment.DoctorId equals Doctor.Id
                                                      join Doctors in db.MDoctors on DoctorOffice.DoctorId equals Doctors.Id
                                                      join MedicalFacility in db.MMedicalFacilities on DoctorOffice.MedicalFacilityId equals MedicalFacility.Id
                                                      join Location in db.MLocations on MedicalFacility.LocationId equals Location.Id
                                                      join LocationLevel in db.MLocationLevels on Location.LocationLevelId equals LocationLevel.Id
                                                      where DoctorOffice.IsDelete == false && DoctorTreatment.DoctorId == id 
                                                      && DoctorOffice.EndDate == null || DoctorOffice.EndDate >= DateTime.Now
                                                      orderby DoctorOffice.Id descending
                                                      select new VMLocation
                                                      {
                                                          DoctorId = DoctorOffice.DoctorId,
                                                          Price = DoctorOfficeTreatmentPrice.Price,
                                                          MedicalFacilityId = MedicalFacility.Id,
                                                          MedicalFacilityName = MedicalFacility.Name,
                                                          Specialization = DoctorOffice.Specialization,
                                                          Location = Location.Name,
                                                          FullAddress = MedicalFacility.FullAddress,
                                                          StartDate = DoctorOffice.StartDate,
                                                          EndDate = DoctorOffice.EndDate

                                                      }).ToList(),

                                          Schedule = (from MedicalFacilitySchedule in db.MMedicalFacilitySchedules
                                                      join MedicalFacility in db.MMedicalFacilities on MedicalFacilitySchedule.MedicalFacilityId equals MedicalFacility.Id
                                                      join DoctorOffice in db.TDoctorOffices on MedicalFacility.Id equals DoctorOffice.MedicalFacilityId

                                                      where MedicalFacilitySchedule.IsDelete == false && MedicalFacility.IsDelete == false
                                                      && DoctorOffice.DoctorId == id
                                                      select new VMSchedule
                                                      {
                                                          MedicalFacilityScheduleId = MedicalFacility.Id,
                                                          DoctorId = DoctorOffice.DoctorId,
                                                          Day = MedicalFacilitySchedule.Day,
                                                          StarTime = MedicalFacilitySchedule.TimeScheduleStart,
                                                          EndTime = MedicalFacilitySchedule.TimeScheduleEnd

                                                      }).ToList(),

                                          LocationHistory = (from DoctorOfficeTreatments in db.TDoctorOfficeTreatments
                                                             join DoctorTreatment in db.TDoctorTreatments on DoctorOfficeTreatments.DoctorTreatmentId equals DoctorTreatment.Id
                                                             join DoctorOffice in db.TDoctorOffices on DoctorOfficeTreatments.DoctorOfficeId equals DoctorOffice.Id
                                                             join Doctor in db.MDoctors on DoctorTreatment.DoctorId equals Doctor.Id
                                                             join Doctors in db.MDoctors on DoctorOffice.DoctorId equals Doctors.Id
                                                             join MedicalFaciliy in db.MMedicalFacilities on DoctorOffice.MedicalFacilityId equals MedicalFaciliy.Id
                                                             join Location in db.MLocations on MedicalFaciliy.LocationId equals Location.Id
                                                             join LocationLevel in db.MLocationLevels on Location.LocationLevelId equals LocationLevel.Id
                                                             where DoctorOffice.IsDelete == false && DoctorTreatment.DoctorId == id
                                                             orderby DoctorOffice.Id descending
                                                             select new VMLocationHistory
                                                             {
                                                                 DoctorId = DoctorOffice.DoctorId,

                                                                 Specialization = DoctorOffice.Specialization,
                                                                 Location = Location.Name,
                                                                 LocationLevel = LocationLevel.Name,
                                                                 StartDate = DoctorOffice.StartDate,
                                                                 EndDate = DoctorOffice.EndDate,
                                                                 Experience = currentYear - DoctorOffice.StartDate.Year

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
                                                                 EndYear = DoctorEducation.EndYear

                                                             }).ToList()
                                      }).FirstOrDefault()!;

            return data;
        }


    }
}
