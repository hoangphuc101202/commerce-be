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
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoriesService>();
        }
    }
}
