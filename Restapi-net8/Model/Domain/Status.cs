﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("status")]
    public class Status : BaseDomain
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column("description", TypeName = "nvarchar(300)")]
        public string? Description { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
