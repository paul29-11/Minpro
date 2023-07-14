﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MinPro.datamodels
{
    [Table("m_user")]
    public partial class MUser
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("biodata_id")]
        public long? BiodataId { get; set; }
        [Column("role_id")]
        public long? RoleId { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Password { get; set; }
        [Column("login_attempt")]
        public int? LoginAttempt { get; set; }
        [Column("is_locked")]
        public bool? IsLocked { get; set; }
        [Column("last_login", TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }
    }
}
