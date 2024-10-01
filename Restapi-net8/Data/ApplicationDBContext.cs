using Microsoft.EntityFrameworkCore;
using Restapi_net8.Model.Domain;
using System.Data;

namespace Restapi_net8.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}
