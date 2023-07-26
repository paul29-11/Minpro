using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMTreatmentPrice
    {
        public long Id { get; set; }
       
        public long? DoctorOfficeTreatmentId { get; set; }
       
        public decimal? Price { get; set; }
        
        public decimal? PriceStartFrom { get; set; }
        
        public decimal? PriceUntilFrom { get; set; }
       
        public long CreatedBy { get; set; }
       
        public DateTime CreatedOn { get; set; }
      
        public long? ModifiedBy { get; set; }
      
        public DateTime? ModifiedOn { get; set; }
       
        public long? DeletedBy { get; set; }
       
        public DateTime? DeletedOn { get; set; }
        
        public bool IsDelete { get; set; }
    }
}
