using System.ComponentModel.DataAnnotations.Schema;
using Restapi_net8.Model.Domain;

[Table("shipping_status")]
public class ShippingStatus : BaseDomain
{
    [Column("name", TypeName = "nvarchar(200)")]
    public string Name { get; set; }
    [Column("description", TypeName = "nvarchar(300)")]
    public string Description { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }
}