using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("customer")]
    public class Customer : BaseDomain
    {
        [Column("full_name", TypeName = "varchar(100)")]
        public string FullName { get; set; }

        [Column("gender", TypeName = "varchar(50)")]
        public string Gender { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Column("birth_of_date", TypeName = "datetime2")]
        public DateTime? BirthOfDate { get; set; }

        [Column("address", TypeName = "varchar(200)")]
        public string? Address { get; set; }

        [Column("phone", TypeName = "varchar(11)")]
        public string? Phone { get; set; }

        [Column("image_url", TypeName = "varchar(max)")]
        public string? ImageUrl { get; set; }

        [Column("is_active", TypeName = "bit")]
        public bool IsActive { get; set; } = true;
            
        [Column("password", TypeName = "varchar(200)")]
        public string Password { get; set; }

        [Column("role", TypeName = "varchar(50)")]
        public string? Role { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}
