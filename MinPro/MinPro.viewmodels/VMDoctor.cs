﻿using System;
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
        public long Id { get; set; }
        public long? BiodataId { get; set; }
        public string NameDoctor { get; set; } = null!;
        public string NameSpecialis { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string? Str { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDelete { get; set; }
    }
}