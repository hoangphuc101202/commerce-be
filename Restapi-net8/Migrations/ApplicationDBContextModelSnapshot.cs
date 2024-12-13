﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restapi_net8.Data;

#nullable disable

namespace Restapi_net8.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restapi_net8.Model.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryAliasName")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("category_alias_name");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("image_url");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("BirthOfDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birth_of_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("gender");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("image_url");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsVerify")
                        .HasColumnType("bit")
                        .HasColumnName("is_verify");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(11)")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("customer");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                            Email = "admin@gmail.com",
                            FullName = "Admin",
                            Gender = "male",
                            IsActive = true,
                            IsDeleted = false,
                            IsVerify = false,
                            Password = "GaNodgXEDqXtPGbzdu6NxA==;u3PPeju79362KCDvOP9UVBN7DwrVjFb/xLsYlf+QVqU=",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("cancel_date");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer_id");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("delivery_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("note");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("order_date");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("payment_method");

                    b.Property<double?>("ShippingFee")
                        .HasColumnType("float")
                        .HasColumnName("shipping_fee");

                    b.Property<Guid?>("ShippingStatusId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("shipping_status_id");

                    b.Property<Guid?>("StatusId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("status_id");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_amount");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ShippingStatusId");

                    b.HasIndex("StatusId");

                    b.ToTable("invoice");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.InvoiceDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("invoice_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("invoice_detail");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("amount");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer_id");

                    b.Property<Guid?>("InvoiceId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("invoice_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("payment_date");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("payment_method");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("payment");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("description");

                    b.Property<double?>("Discount")
                        .HasColumnType("float")
                        .HasColumnName("discount");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("image_url");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<DateTime?>("ProductDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("product_date");

                    b.Property<string>("ProductNameAlias")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("product_name_alias");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("supplier_id");

                    b.Property<long?>("Views")
                        .HasColumnType("bigint")
                        .HasColumnName("views");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("status");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                            Description = "Đơn hàng đang chờ thanh toán",
                            IsDeleted = false,
                            Name = "Chờ thanh toán"
                        },
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"),
                            Description = "Đơn hàng đã thanh toán",
                            IsDeleted = false,
                            Name = "Đơn hàng đã thanh toán"
                        });
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("address");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Logo")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("logo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(11)")
                        .HasColumnName("phone");

                    b.Property<string>("SupplierContact")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("supplier_contact");

                    b.HasKey("Id");

                    b.ToTable("supplier");
                });

            modelBuilder.Entity("ShippingStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("shipping_status");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                            Description = "Đơn hàng đang chờ xác nhận",
                            IsDeleted = false,
                            Name = "Chờ xác nhận"
                        },
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"),
                            Description = "Đơn hàng đang giao hàng",
                            IsDeleted = false,
                            Name = "Đang giao hàng"
                        },
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                            Description = "Đơn hàng đã giao hàng",
                            IsDeleted = false,
                            Name = "Đã giao hàng"
                        },
                        new
                        {
                            Id = new Guid("c62e3c10-5e07-427e-a55a-45cd301b5397"),
                            Description = "Đơn hàng đã hủy",
                            IsDeleted = false,
                            Name = "Đã hủy"
                        });
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Invoice", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShippingStatus", "ShippingStatus")
                        .WithMany("Invoices")
                        .HasForeignKey("ShippingStatusId");

                    b.HasOne("Restapi_net8.Model.Domain.Status", "Status")
                        .WithMany("Invoices")
                        .HasForeignKey("StatusId");

                    b.Navigation("Customer");

                    b.Navigation("ShippingStatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.InvoiceDetail", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restapi_net8.Model.Domain.Product", "Product")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Payment", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Product", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Restapi_net8.Model.Domain.Supplier", null)
                        .WithMany("Products")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Customer", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Product", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Status", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Supplier", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShippingStatus", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
