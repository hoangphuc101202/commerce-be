using System.ComponentModel.DataAnnotations;

public class GetAllInvoiceRequest{
    public int? page { get; set; } 
    public int? limit { get; set; } 
    public string? startDate { get; set; }
    public string? endDate { get; set; }
    public string? statusShipping { get; set; } 
    
}