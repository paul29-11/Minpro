﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMLocation
    {
        public long? DoctorId { get; set; }
        public long? MedicalFacilityId { get; set; }
        public long? MedicalFacilityScheduleId { get; set; }
        public string? MedicalFacilityName { get; set; }
        public string? Specialization { get; set; } = null!;
        public string? Location { get; set; }
        public string? StarTime { get; set; }
        public string? EndTime { get; set; }
        public decimal? Price { get; set; }
        public string? Day { get; set; }
        public string? FullAddress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
        public List<VMSchedule>? Schedule { get; set; }
    }
}
