using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMSchedule
    {
        public long? DoctorId { get; set; }
        public long? MedicalFacilityId { get; set; }
        public long? MedicalFacilityScheduleId { get; set; }
        public string? StarTime { get; set; }
        public string? EndTime { get; set; }
        public string? Day { get; set; }
        public string? FullAddress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
