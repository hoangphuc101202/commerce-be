using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("product")]
    public class Product : BaseDomain
    {
        [Column("name", TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Column("product_name_alias", TypeName = "nvarchar(200)")]
        public string? ProductNameAlias { get; set; }

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public float? Price { get; set; }

        [Column("image_url", TypeName = "varchar(max)")]
        public string? ImageUrl { get; set; }

        [Column("product_date", TypeName = "datetime2")]
        public DateTime? ProductDate { get; set; }

        [Column("discount", TypeName = "float")]
        public float? Discount { get; set; }

        [Column("views", TypeName = "bigint")]
        public long? Views { get; set;  }

        [Column("description", TypeName = "nvarchar(300)")]
        public string? Description { get; set; }

        [ForeignKey("Supplier")]
        public Guid? SupplierId { get; set; }
    }
}
