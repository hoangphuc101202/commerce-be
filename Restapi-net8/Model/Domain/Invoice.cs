﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Restapi_net8.Model.Domain
{
    [Table("invoice")]
    public class Invoice : BaseDomain
    {
        [Column("order_date", TypeName = "datetime2")]
        public DateTime? OrderDate { get; set; } = DateTime.Now;

        [Column("delivery_date", TypeName = "datetime2")]
        public DateTime? DeliveryDate { get; set; }

        [Column("cancel_date", TypeName = "datetime2")]
        public DateTime? CancelDate { get; set; }

        [Column("address", TypeName = "nvarchar(200)")]
        public string? Address { get; set; }

        [Column("payment_method", TypeName = "varchar(200)")]
        public string? PaymentMethod { get; set; }

        [Column("shipping_fee", TypeName = "float")]
        public float? ShippingFee { get; set; }

        [ForeignKey("Customer")]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [ForeignKey("Status")]
        [Column("status_id")]
        public Guid? StatusId { get; set; }

        [Column("note", TypeName = "nvarchar(300)")]
        public string? Note { get; set; }

        [ForeignKey("ShippingStatus")]
        [Column("shipping_status_id")]
        public Guid? ShippingStatusId { get; set; }
        [Column("total_amount", TypeName = "decimal(18,2)")]
        public decimal? TotalAmount { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Status Status { get; set; }
        public virtual ShippingStatus ShippingStatus { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
