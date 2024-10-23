using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("category")]
    public class Category : BaseDomain
    {
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column("category_alias_name", TypeName = "varchar(100)")]
        public string? CategoryAliasName { get; set; }

        [Column("description", TypeName = "varchar(300)")]
        public string? Description { get; set; }

        [Column("image_url", TypeName = "varchar(max)")]
        public string? ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
