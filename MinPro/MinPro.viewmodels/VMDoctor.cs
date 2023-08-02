using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMDoctor
    {
        public long? DoctorId { get; set; }
        public long? BiodataId { get; set; }
        public string? Str { get; set; }
        public long? SpecializationId { get; set; }
        public string? SpecializationName { get; set; }
        public decimal? Price { get; set; }
        public string? Fullname { get; set; }
        public string? MobilePhone { get; set; }
        public int? CountAppointment { get; set; }
        public byte[]? Image { get; set; }
        public string ImagePath { get; set; } =  null!;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        //public string DoctorImage { get; set; }
        public VMTreatmentPrice? TreatmentPrice { get; set; }
        public List<VMDoctorTreatment>? Treatment { get; set; }
        public List<VMLocation>? Location { get; set; }
        public List<VMLocationHistory>? LocationHistory { get; set; }
        public List<VMDoctorEducation>? DoctorEducation { get; set; }
        public VMMedicalItem? Chat { get; set; }

    }
}
