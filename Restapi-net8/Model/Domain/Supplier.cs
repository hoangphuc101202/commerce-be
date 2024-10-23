using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("supplier")]
    public class Supplier : BaseDomain
    {
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("logo", TypeName = "varchar(max)")]
        public string? Logo { get; set; }

        [Column("supplier_contact", TypeName = "varchar(100)")]
        public string? SupplierContact { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Column("phone", TypeName = "varchar(11)")]
        public string? Phone { get; set; }

        [Column("address", TypeName = "varchar(200)")]
        public string? Address { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
