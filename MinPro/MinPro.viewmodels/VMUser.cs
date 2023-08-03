using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMUser
    {
        public long? Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public long? IdRole { get; set; }
        public string ImagePath { get; set; } = null!;
    }
}
