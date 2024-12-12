using System.Globalization;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Restapi_net8.Exceptions.Http;
using Restapi_net8.Middlewares;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;
using Serilog;

public class InvoiceService : IInvoiceService
{
    private readonly IProductRepository _productRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceDetailRepository _invoiceDetailRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IShippingStatusRepository _shippingStatusRepository;
    public InvoiceService(IShippingStatusRepository shippingStatusRepository,IProductRepository productRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository, IStatusRepository statusRepository)
    {
        _productRepository = productRepository;
        _invoiceRepository = invoiceRepository;
        _invoiceDetailRepository = invoiceDetailRepository;
        _statusRepository = statusRepository;
        _shippingStatusRepository = shippingStatusRepository;
    }
    private decimal CalculateDiscoutPrice(decimal price, decimal discount)
    {
        return (decimal)(price - (discount * price) / 100);
    }
    public async Task<ApiResponse> Checkout(CheckoutRequest request, string userId)
    {
        decimal totalAmount = 0;
     
        foreach (var item in request.cartItems)
        {
            decimal productDiscount = 0;
            decimal productPrice = 0;
            var product = await _productRepository.GetById(Guid.Parse(item.productId));
            if (product == null)
            {
                throw new NotFoundHttpException($"Product with id {item.productId} not found");
            }
            if ((product.Views ?? 0) < item.quantity)
            {
                throw new BadHttpRequestException($"Product with id {item.productId} has only {product.Views ?? 0} stock left");
            }
            productDiscount = (decimal)((product.Discount ?? 0) * product.Price) / 100;
            productPrice = ((decimal)product.Price - productDiscount) * item.quantity;
            totalAmount += productPrice;
        }
        totalAmount += 25000; // Shipping fee
        var cartItems = new List<CartItem>();
        foreach (var item in request.cartItems)
        {
            cartItems.Add(new CartItem
            {
                productId = item.productId,
                productName = _productRepository.GetById(Guid.Parse(item.productId)).Result.Name,
                quantity = item.quantity,
                discount = (decimal)(_productRepository.GetById(Guid.Parse(item.productId)).Result.Discount ?? 0)
            });
        }
        var dataCheckout = new ResponseCheckout
        {
            totalAmount = totalAmount,
            shippingFee = 25000,
            cartItems = cartItems
        };
        return new ApiResponse(200, "Checkout successfully", dataCheckout, null);
    }

    public async Task<ApiResponse> Order(OrderRequest request, string userId)
    {
        var paymentMethod = request.paymentMethod;
        if (paymentMethod == 0 ){
            foreach (var item in request.cartItems)
        {
            var product = await _productRepository.GetById(Guid.Parse(item.productId));
            if (product == null)
            {
                throw new NotFoundHttpException($"Product with id {item.productId} not found");
            }
            if ((product.Views ?? 0) < item.quantity)
            {
                throw new BadHttpRequestException($"Product with id {item.productId} has only {product.Views ?? 0} stock left");
            }
            long newViews = (long)product.Views - item.quantity;
            var viewToUpdated = new Product{
                Views = newViews
            };
            await _productRepository.UpdateAsync(product, viewToUpdated);
        }
        var invoice = new Invoice
        {
            CustomerId = Guid.Parse(userId),
            StatusId = Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4395"),
            Address = request.address,
            PaymentMethod = paymentMethod.ToString(), // 0 = COD, 1 = Bank Transfer
            ShippingFee = 25000,
            Note = request.note ?? "",
            ShippingStatusId = Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4395"),
            TotalAmount = request.totalAmount
        };
        await _invoiceRepository.CreateAsync(invoice);
        foreach (var item in request.cartItems)
        {
            decimal productPrice = (decimal)_productRepository.GetById(Guid.Parse(item.productId)).Result.Price;
            decimal productDiscount = (decimal)(_productRepository.GetById(Guid.Parse(item.productId)).Result.Discount ?? 0);
            var invoiceDetail = new InvoiceDetail
            {
                InvoiceId = invoice.Id,
                ProductId = Guid.Parse(item.productId),
                Quantity = item.quantity,
                Price = (double)CalculateDiscoutPrice(productPrice, productDiscount),
            };
            await _invoiceDetailRepository.CreateAsync(invoiceDetail);
        }
        
            return new ApiResponse(200, "Order successfully", null, null);
        }
        else {
            return new ApiResponse(400, "Payment method is banking", null, null);
        }
    }
    public async Task<ApiResponse> GetStatus(){
        var status = await _statusRepository.GetAll();
        var data = status.Select(s => new StatusDTO
        {
            id = s.Id.ToString(),
            name = s.Name
        }).ToList();
        return new ApiResponse(200, "Get status successfully", data, null);
    }
    public async Task<ApiResponse>GetShippingStatus(){
        var status = await _shippingStatusRepository.GetAll();
        var data = status.Select(s => new StatusDTO
        {
            id = s.Id.ToString(),
            name = s.Name
        }).ToList();
        return new ApiResponse(200, "Get status shipping successfully", data, null);
    }
    public async Task<ApiResponse> GetAll(GetAllInvoiceRequest request)
    {
        var limit = request.limit ?? 12;
        var page = request.page ?? 1;
        var startDate = request.startDate ?? null;
        var endDate = request.endDate ?? null;
        var status = request.statusShipping ?? null;
        if (!string.IsNullOrEmpty(startDate) || !string.IsNullOrEmpty(endDate)){
            if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                throw new BadRequestHttpException("Start date is invalid. It must be in the format YYYY-MM-DD.");
            }
        }
        if (!string.IsNullOrEmpty(status)){
            var validStatuses = new[] 
            { 
                "Chờ xác nhận", 
                "Đang giao hàng", 
                "Đã giao hàng", 
                "Đã hủy" 
            };
            if (!validStatuses.Contains(status))
            {
                throw new BadRequestHttpException(
                    "Status is invalid. It must be in the format Chờ xác nhận, Đang giao hàng, Đã giao hàng, Đã hủy."
                );
            }
        }
        var invoices = await _invoiceRepository.GetAllInvoiceWithPage(limit, page, startDate, endDate, status);
        var data = invoices.Select(i => new InvoiceDTO
        {
            id = i.Id.ToString(),
            orderDate = i.OrderDate.ToString(),
            totalAmount = i.TotalAmount ?? 0,
            status = i.Status.Name,
            shippingStatus = i.ShippingStatus.Name,
            customeName = i.Customer.FullName,
            customerId = i.CustomerId.ToString(),
            cancelDate = i.CancelDate.ToString() ?? "",
            shippingDate =  i.DeliveryDate.ToString()  ?? "",
            paymentMethod = i.PaymentMethod == "0" ? "Thanh toán khi nhận hàng" : "Thanh toán bằng VNPay"
        }).ToList();

        var result = new {
            data = data,
            total = data.Count(),
            limit = limit,
            page = page
        };
        return new ApiResponse(200, "Get all invoices successfully", result, null);
         
    }

    public async Task<ApiResponse> getOrderOfUser(Guid id)
    {
        try{
            var invoices = await _invoiceRepository.GetByConditionAsync(id);
            if(invoices.Count() == 0){
                return new ApiResponse(404, "User has no order", null, null);
            }
            var data = invoices.Select(i => new InvoiceDTO{
                id = i.Id.ToString(),
                orderDate = i.OrderDate.ToString(),
                totalAmount = i.TotalAmount ?? 0,
                status = i.Status.Name,
                shippingStatus = i.ShippingStatus.Name,
                cancelDate = i.CancelDate.ToString() ?? "",
                shippingDate =  i.DeliveryDate.ToString()  ?? "",
                paymentMethod = i.PaymentMethod == "0" ? "Thanh toán khi nhận hàng" : "Thanh toán bằng VNPay"
            });
            return new ApiResponse(200, "Get order of user successfully", data, null);
        }
        catch(Exception e){
            Log.Error(e, "Error when get order of user");
            throw new InternalServerErrorHttpException("Error when get order of user");
        }
    }
    public async Task<ApiResponse> GetInvoice(string id, string role, string userId){
        if (role == "Admin") {
            var invoice = await _invoiceRepository.GetInvoiceId(Guid.Parse(id));
            if(invoice == null){
                throw new NotFoundHttpException($"Invoice with id {id} not found");
            }
            var invoiceDetails = await _invoiceDetailRepository.GetByInvoiceId(Guid.Parse(id));
            var details = invoiceDetails.Select(i => new InvoiceDetailDTO
            {
                productName = i.Product.Name,
                quantity = i.Quantity ?? 0,
                price = i.Price ?? 0
            }).ToList();
            var data = new {
                id = invoice.Id.ToString(),
                orderDate = invoice.OrderDate.ToString(),
                totalAmount = invoice.TotalAmount ?? 0,
                status = invoice.Status.Name,
                shippingStatus = invoice.ShippingStatus.Name,
                customeName = invoice.Customer.FullName,
                customerId = invoice.CustomerId.ToString(),
                cancelDate = invoice.CancelDate.ToString() ?? "",
                shippingDate =  invoice.DeliveryDate.ToString()  ?? "",
                paymentMethod = invoice.PaymentMethod == "0" ? "Thanh toán khi nhận hàng" : "Thanh toán bằng VNPay",
                invoiceDetil = details
            };
            return new ApiResponse(200, "Get invoice successfully", data, null);
        }
        else {
            var invoice = await _invoiceRepository.GetInvoiceId(Guid.Parse(id));
            if(invoice == null){
                throw new NotFoundHttpException($"Invoice with id {id} not found");
            }
            if(invoice.CustomerId.ToString() != userId){
                throw new BadRequestHttpException("You do not have permission to access this invoice");    
            }
            var invoiceDetails = await _invoiceDetailRepository.GetByInvoiceId(Guid.Parse(id));
            var details = invoiceDetails.Select(i => new InvoiceDetailDTO
            {
                productName = i.Product.Name,
                quantity = i.Quantity ?? 0,
                price = i.Price ?? 0
            }).ToList();
            var data = new {
                id = invoice.Id.ToString(),
                orderDate = invoice.OrderDate.ToString(),
                totalAmount = invoice.TotalAmount ?? 0,
                status = invoice.Status.Name,
                shippingStatus = invoice.ShippingStatus.Name,
                cancelDate = invoice.CancelDate.ToString() ?? "",
                shippingDate =  invoice.DeliveryDate.ToString()  ?? "",
                paymentMethod = invoice.PaymentMethod == "0" ? "Thanh toán khi nhận hàng" : "Thanh toán bằng VNPay",
                invoiceDetil = details
            };
            return new ApiResponse(200, "Get Detail invoice successfully", data, null);
        }   
    }
    public async Task<ApiResponse> UpdateInvoiceForAdmin(UpdateInvoiceRequest request, string id){
        var invoice = await _invoiceRepository.GetInvoiceId(Guid.Parse(id));
        if(invoice == null){
            throw new NotFoundHttpException($"Invoice with id {id} not found");
        }
        if(request.shippingStatus != null){
            var shippingStatusExist = await _shippingStatusRepository.GetById(Guid.Parse(request.shippingStatus));
            if(shippingStatusExist == null){
                throw new NotFoundHttpException($"Shipping status with id {request.shippingStatus} not found");
            }           
            if(invoice.ShippingStatus.Id == Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4396")){
                if(shippingStatusExist.Name == "Đã hủy"){
                    throw new BadRequestHttpException("Invoice delivering, cannot be canceled");
                }
            }
            if(shippingStatusExist.Name == "Đang giao hàng" || shippingStatusExist.Name == "Đã giao hàng"){
                if(invoice.ShippingStatus.Name == "Đã hủy"){
                    throw new BadRequestHttpException("Invoice has been canceled, cannot be delivered");
                }
            }
            if(shippingStatusExist.Name == "Đã giao hàng"){
                invoice.DeliveryDate = DateTime.Now;
            }
            if(shippingStatusExist.Name == "Đã hủy"){
                if(invoice.Status.Id == Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4396"))
                {
                    throw new BadRequestHttpException("Invoice has been paid, cannot be canceled");
                }
                if(invoice.ShippingStatus.Id == Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4397"))
                {
                    throw new BadRequestHttpException("Invoice has been delivered, cannot be canceled");
                }
                invoice.CancelDate = DateTime.Now;
            }
            invoice.ShippingStatusId = Guid.Parse(request.shippingStatus);
        }
        await _invoiceRepository.UpdateAsync(invoice, invoice);
        return new ApiResponse(200, "Update invoice successfully", null, null);
    }
    public async Task<ApiResponse> UpdateInvoiceForUser(UpdateInvoiceRequestForUser request, string id, string userId ){
        var invoice = await _invoiceRepository.GetInvoiceId(Guid.Parse(id));
        if(invoice == null){
            throw new NotFoundHttpException($"Invoice with id {id} not found");
        }
        if(invoice.CustomerId.ToString() != userId){
            throw new BadRequestHttpException("You do not have permission to access this invoice");
        }
        if(request.shippingStatus != null){
            var shippingStatusExist = await _shippingStatusRepository.GetById(Guid.Parse(request.shippingStatus));
            if(shippingStatusExist == null){
                throw new NotFoundHttpException($"Shipping status with id {request.shippingStatus} not found");
            }
            if(shippingStatusExist.Name == "Đang giao hàng" || shippingStatusExist.Name == "Đã giao hàng"){
                throw new BadRequestHttpException("Cannot update shipping status");
            }
            if(invoice.ShippingStatus.Id == Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4396")){
                if(shippingStatusExist.Name == "Đã hủy"){
                    throw new BadRequestHttpException("Invoice delivering, cannot be canceled");
                }
            }
            if(shippingStatusExist.Name == "Đã hủy"){
                if(invoice.Status.Id == Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4396"))
                {
                    throw new BadRequestHttpException("Invoice has been paid, cannot be canceled");
                }
                if(invoice.ShippingStatus.Id == Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4397"))
                {
                    throw new BadRequestHttpException("Invoice has been delivered, cannot be canceled");
                }
                invoice.CancelDate = DateTime.Now;
            }
            invoice.ShippingStatusId = Guid.Parse(request.shippingStatus);
        }
        await _invoiceRepository.UpdateAsync(invoice, invoice);
        return new ApiResponse(200, "Update invoice successfully", null, null);
    }
}