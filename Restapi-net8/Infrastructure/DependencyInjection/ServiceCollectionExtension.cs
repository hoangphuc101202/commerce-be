using Restapi_net8.Infrastructure.Authentication;
using Restapi_net8.Infrastructure.Mapping;
using Restapi_net8.Infrastructure.Password;
using Restapi_net8.Middlewares;
using Restapi_net8.Repository.Implementation;
using Restapi_net8.Repository.Interface;
using Restapi_net8.Services.Implementation;
using Restapi_net8.Services.Interface;
namespace Restapi_net8.Infrastructure.DependencyInjection

{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IShippingStatusRepository, ShippingStatusRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();  
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoriesService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISupplierService, SupplierService>();
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
        public static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<HttpExceptionHandlingMiddleware>();
        }
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<PasswordHasher>();
            services.AddSingleton<TokenProvider>();
            services.AddScoped<MailService>();
        }
    }
}
