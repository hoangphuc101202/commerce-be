using Microsoft.EntityFrameworkCore;
using Restapi_net8.Model.Domain;
using System.Data;

namespace Restapi_net8.Data
{
    public class ApplicationDBContext : DbContext
    {   
        private readonly IConfiguration configuration;
        public ApplicationDBContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
                new Customer{
                    Id = new Guid("C62E3C10-5E07-427E-A55A-45CD301B4395"),
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    Password = configuration["AppSettings:PasswordAdmin"],
                    Gender = "male",
                    Role = "Admin",
                }
            );
        }

    }
}
