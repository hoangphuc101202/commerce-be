using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("invoice_detail")]
    public class InvoiceDetail : BaseDomain
    {
        [ForeignKey("Invoice")]
        [Column("invoice_id")]
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [ForeignKey("Product")]  
        [Column("product_id")]       
        public Guid ProductId { get; set; }
        [Column("quantity", TypeName = "int")] 
        public int? Quantity { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public double? Price { get; set; }

    }
}
