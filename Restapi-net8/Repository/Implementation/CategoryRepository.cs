using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

namespace Restapi_net8.Repository.Implementation
{
    public class CategoryRepository : BaseRepository<Category> ,ICategoryRepository
    {
        public CategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

    }
}
