using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("employee")]
    public class Employee : BaseDomain
    {
        [Column("full_name", TypeName = "nvarchar(100)")]
        public string FullName { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(200)")]
        public string Password { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
