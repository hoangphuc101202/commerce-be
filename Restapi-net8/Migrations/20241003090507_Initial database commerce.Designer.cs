﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restapi_net8.Data;

#nullable disable

namespace Restapi_net8.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241003090507_Initial database commerce")]
    partial class Initialdatabasecommerce
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("varchar(300)")
                        .HasColumnName("description");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("image_url");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
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
                        .HasColumnType("varchar(200)")
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
                        .HasColumnType("varchar(100)")
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
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CancleDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("cancle_date");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("delivery_date");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Note")
                        .HasColumnType("varchar(300)")
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

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StatusId");

                    b.ToTable("invoice");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.InvoiceDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Discount")
                        .HasColumnType("float")
                        .HasColumnName("discount");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("invoice_detail");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(300)")
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
                        .HasColumnType("varchar(200)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<DateTime?>("ProductDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("product_date");

                    b.Property<string>("ProductNameAlias")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("product_name_alias");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Views")
                        .HasColumnType("bigint")
                        .HasColumnName("views");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(300)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("status");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(200)")
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
                        .HasColumnType("varchar(100)")
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

            modelBuilder.Entity("Restapi_net8.Model.Domain.Invoice", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Customer", null)
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restapi_net8.Model.Domain.Employee", null)
                        .WithMany("Invoices")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restapi_net8.Model.Domain.Status", null)
                        .WithMany("Invoices")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.InvoiceDetail", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restapi_net8.Model.Domain.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Product", b =>
                {
                    b.HasOne("Restapi_net8.Model.Domain.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restapi_net8.Model.Domain.Supplier", null)
                        .WithMany("Products")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Customer", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Employee", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Status", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Restapi_net8.Model.Domain.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
