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
    public InvoiceService(IProductRepository productRepository, IInvoiceRepository invoiceRepository, IInvoiceDetailRepository invoiceDetailRepository)
    {
        _productRepository = productRepository;
        _invoiceRepository = invoiceRepository;
        _invoiceDetailRepository = invoiceDetailRepository;
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
            // long newViews = (long)product.Views - item.quantity;
            // var viewToUpdated = new Product{
            //     Views = newViews
            // };
            // await _productRepository.UpdateAsync(product, viewToUpdated);
            totalAmount += productPrice;
        }
        totalAmount += 25000; // Shipping fee
        // var invoice = new Invoice
        // {
        //     CustomerId = Guid.Parse(userId),
        //     StatusId = Guid.Parse("C62E3C10-5E07-427E-A55A-45CD301B4395"),
        //     Address = request.address,
        //     PaymentMethod = request.paymentMethod,
        //     ShippingFee = 25000,
        //     Note = request.note ?? "",
        // };
        // await _invoiceRepository.CreateAsync(invoice);
        // foreach (var item in request.cartItems)
        // {
        //     decimal productPrice = (decimal)_productRepository.GetById(Guid.Parse(item.productId)).Result.Price;
        //     decimal productDiscount = (decimal)(_productRepository.GetById(Guid.Parse(item.productId)).Result.Discount ?? 0);
        //     var invoiceDetail = new InvoiceDetail
        //     {
        //         InvoiceId = invoice.Id,
        //         ProductId = Guid.Parse(item.productId),
        //         Quantity = item.quantity,
        //         Price = (double)CalculateDiscoutPrice(productPrice, productDiscount),
        //     };
        //     await _invoiceDetailRepository.CreateAsync(invoiceDetail);
        // }
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
        throw new NotImplementedException();
    }
}