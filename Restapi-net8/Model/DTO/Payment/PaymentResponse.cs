public class PaymentResponse {
    public string id { get; set; }
    public string paymentMethod { get; set; }
    public decimal amount { get; set; }
    public string customerId { get; set; }
    public string invoiceId { get; set; }
    public string paymentDate { get; set; }
    public string? customerName  { get; set; }

}