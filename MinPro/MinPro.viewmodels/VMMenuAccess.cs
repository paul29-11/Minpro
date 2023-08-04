using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinPro.viewmodels
{
    public class VMMenuAccess
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string? MenuName { get; set; }
        public string? MenuController { get; set; }
        public string? MenuAction { get; set; }
        public string? MenuUrl { get; set; }
        public string? MenuIcon { get; set; }
        public int? MenuSorting { get; set; }
        public bool? IsParent { get; set; }
        public long? MenuParent { get; set; }
        public long? IdRole { get; set; }
        public string? RoleName { get; set; }
        public long? IdMenu { get; set; }
        public bool is_selected { get; set; }

        public List<VMMenuAccess>? ListChild { get; set; }

    }
}
