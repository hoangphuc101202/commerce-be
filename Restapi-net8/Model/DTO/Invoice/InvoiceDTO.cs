public class InvoiceDTO{
    public string id { get; set; }
    public string orderDate { get; set; }
    public string shippingDate { get; set; }
    public string deliveryDate { get; set; }
    public string cancelDate { get; set; }
    public string status { get; set; }
    public string shippingStatus { get; set; }
    public decimal totalAmount { get; set; }

    public string customeName { get; set; }
}