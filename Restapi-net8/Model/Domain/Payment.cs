using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain;

[Table("payment")]
public class Payment : BaseDomain
{
    [Column("payment_date", TypeName = "datetime2")]
    public DateTime? PaymentDate { get; set; }
    [Column("amount", TypeName = "decimal(18,2)")]
    public double? Amount { get; set; }
    [Column("payment_method", TypeName = "varchar(200)")]
    public string? PaymentMethod { get; set; }
    [Column("customer_id", TypeName = "uniqueidentifier")]
    public Guid? CustomerId { get; set; }
    [Column("invoice_id", TypeName = "uniqueidentifier")]
    public Guid? InvoiceId { get; set; }

}
