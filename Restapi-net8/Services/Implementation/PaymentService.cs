using System.Globalization;
using Restapi_net8.Exceptions.Http;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Interface;
using Serilog;

public class PaymentService : IPaymentService{
    private readonly IInvoiceRepository invoiceRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IPaymentRepository _paymentRepository;
    public PaymentService(IInvoiceRepository invoiceRepository, IUsersRepository usersRepository, IPaymentRepository paymentRepository){
        this.invoiceRepository = invoiceRepository;
        _usersRepository = usersRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<ApiResponse> CreatePayment(PaymentRequest paymentRequest){
        var invoice = await invoiceRepository.GetInvoiceId(Guid.Parse(paymentRequest.invoiceId));
        if(invoice == null){
            throw new NotFoundHttpException("Invoice not found");
        }
        if(invoice.ShippingStatus.Name != "Đã giao hàng"){
            throw new BadRequestHttpException("Invoice not delivered yet");
        }
        var paymentExist = await _paymentRepository.GetInvoiceIsPaymentId(Guid.Parse(paymentRequest.invoiceId));
        if(paymentExist != null){
            throw new BadRequestHttpException("Payment already exists");
        }
        invoice.StatusId = Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4396");
        await invoiceRepository.UpdateAsync(invoice, invoice);
        var paymentCreate = new Payment{
            InvoiceId = Guid.Parse(paymentRequest.invoiceId),
            CustomerId = invoice.CustomerId,
            PaymentDate = DateTime.Now, 
            PaymentMethod = invoice.PaymentMethod,
            Amount = (double)invoice.TotalAmount,
        };
        await _paymentRepository.CreateAsync(paymentCreate);
        return new ApiResponse(200, "Payment created successfully", null, null);
    }
    public async Task<ApiResponse> GetAll(GetAllPaymentRequest request){
        var limit = request.limit ?? 12;
        var page = request.page ?? 1;
        var startDate = request.startDate ?? null;
        var endDate = request.endDate ?? null;
        if (!string.IsNullOrEmpty(startDate) || !string.IsNullOrEmpty(endDate)){
            if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                throw new BadRequestHttpException("Start date is invalid. It must be in the format YYYY-MM-DD.");
            }
        }
        var payment = await _paymentRepository.GetAllPaymentWithPage(limit, page, startDate, endDate);
        var paymentResponse = payment.Select(p => new PaymentResponse {
            id = p.Id.ToString(),
            invoiceId = p.InvoiceId.ToString(),
            customerId = p.CustomerId.ToString(),
            customerName = p.Customer.FullName,
            paymentDate = p.PaymentDate.ToString(),
            paymentMethod = p.PaymentMethod == "0" ? "Thanh toán khi nhận hàng" : "Thanh toán bằng VNPay",
            amount = (decimal)p.Amount,
        }); 
        var totalPage = await _paymentRepository.GetTotalPage(limit);
        var result = new {
            data = paymentResponse,
            total = paymentResponse.Count(),
            limit = limit,
            page = page,
            totalPage = totalPage,
        };
        return new ApiResponse(200, "Get all payment successfully", result, null);
    }
    public async Task<ApiResponse> GetPaymentByUser(string userId){
        var customer = await _usersRepository.GetById(Guid.Parse(userId));
        if(customer == null){
            throw new NotFoundHttpException("Customer not found");
        }
        var payment = await _paymentRepository.GetPaymentByUser(Guid.Parse(userId));
        var paymentResponse = payment.Select(p => new PaymentResponse {
            id = p.Id.ToString(),
            invoiceId = p.InvoiceId.ToString(),
            customerId = p.CustomerId.ToString(),
            paymentDate = p.PaymentDate.ToString(),
            paymentMethod = p.PaymentMethod == "0" ? "Thanh toán khi nhận hàng" : "Thanh toán bằng VNPay",
            amount = (decimal)p.Amount,
        }).ToList(); 
        return new ApiResponse(200, "Get all payment successfully", paymentResponse, null);
    }
}