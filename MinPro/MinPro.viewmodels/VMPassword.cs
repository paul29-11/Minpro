using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMPassword
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }

        public string? ResetFor { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public long? DeletedBy { get; set; }
 
        public DateTime? DeletedOn { get; set; }

        public bool IsDelete { get; set; }
    }
}
