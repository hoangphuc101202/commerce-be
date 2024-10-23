using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("invoice_detail")]
    public class InvoiceDetail : BaseDomain
    {
        [ForeignKey("Invoice")]
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [ForeignKey("Product")]         
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Column("quantity", TypeName = "int")] 
        public int? Quantity { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public double? Price { get; set; }

        [Column("discount", TypeName = "float")]
        public float? Discount { get; set; }
    }
}
