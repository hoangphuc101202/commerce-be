using Microsoft.EntityFrameworkCore;
using Restapi_net8.Data;
using Restapi_net8.Model.Domain;
using Restapi_net8.Repository.Interface;

namespace Restapi_net8.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext dbContext;

        public CategoryRepository(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
           var categoryToDelete = await dbContext.Categories.FindAsync(id);
           if(categoryToDelete == null)
            {
                throw new Exception("Category not found");
            }
            dbContext.Categories.Remove(categoryToDelete);
            await dbContext.SaveChangesAsync();
            return categoryToDelete;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await dbContext.Categories.ToListAsync();
        }
        
        public async Task<Category?> GetById(Guid id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            var categoryToUpdate = await dbContext.Categories.FindAsync(id);
            if (categoryToUpdate == null)
            {
                throw new Exception("Category not found");
            }
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.UrlHandle = category.UrlHandle;
            dbContext.Categories.Update(categoryToUpdate);
            await dbContext.SaveChangesAsync();
            return categoryToUpdate;
        }
        
    }
}
