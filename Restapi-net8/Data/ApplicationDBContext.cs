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
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ShippingStatus> ShippingStatuses { get; set; }
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
            modelBuilder.Entity<Status>().HasData(
                new Status{
                    Id = new Guid("C62E3C10-5E07-427E-A55A-45CD301B4395"),
                    Name = "Chờ thanh toán",
                    Description = "Đơn hàng đang chờ thanh toán",
                },
                new Status{
                    Id = new Guid("C62E3C10-5E07-427E-A55A-45CD301B4396"),
                    Name = "Đơn hàng đã thanh toán",
                    Description = "Đơn hàng đã thanh toán",
                }       
            );
            modelBuilder.Entity<ShippingStatus>().HasData(
                new ShippingStatus{
                    Id = new Guid("C62E3C10-5E07-427E-A55A-45CD301B4395"),
                    Name = "Chờ xác nhận",
                    Description = "Đơn hàng đang chờ xác nhận",
                },
                new ShippingStatus{
                    Id = new Guid("C62E3C10-5E07-427E-A55A-45CD301B4396"),
                    Name = "Đang giao hàng",
                    Description = "Đơn hàng đang giao hàng",
                },
                new ShippingStatus{
                    Id = new Guid("C62E3C10-5E07-427E-A55A-45CD301B4397"),
                    Name = "Đã giao hàng",
                    Description = "Đơn hàng đã giao hàng",
                }
            );
        }

    }
}
