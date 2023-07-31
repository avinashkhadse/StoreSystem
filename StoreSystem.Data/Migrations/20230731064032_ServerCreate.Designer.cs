﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreSystem.Data;

#nullable disable

namespace StoreSystem.Data.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20230731064032_ServerCreate")]
    partial class ServerCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StoreSystem.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("StoreSystem.Models.Mobile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Mobiles");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.MonthlyBrandWiseSalesReport", b =>
                {
                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalProfitLoss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalSales")
                        .HasColumnType("int");

                    b.HasIndex("BrandId");

                    b.ToTable("MonthlyBrandWiseSalesReports");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.MonthlySalesReportItem", b =>
                {
                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalProfitLoss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalSales")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.ToTable("MonthlySalesReportItems");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.SaleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MobileId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SalesId")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MobileId");

                    b.HasIndex("SalesId");

                    b.ToTable("SaleItems");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.Sales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MobileId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MobileId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("StoreSystem.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StoreSystem.Models.Mobile", b =>
                {
                    b.HasOne("StoreSystem.Models.Brand", "Brand")
                        .WithMany("Mobiles")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.MonthlyBrandWiseSalesReport", b =>
                {
                    b.HasOne("StoreSystem.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.SaleItem", b =>
                {
                    b.HasOne("StoreSystem.Models.Mobile", "Mobile")
                        .WithMany("SaleItems")
                        .HasForeignKey("MobileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StoreSystem.Models.Reports.Sales", "Sales")
                        .WithMany("SaleItems")
                        .HasForeignKey("SalesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Mobile");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.Sales", b =>
                {
                    b.HasOne("StoreSystem.Models.Mobile", "Mobile")
                        .WithMany()
                        .HasForeignKey("MobileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mobile");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StoreSystem.Models.Brand", b =>
                {
                    b.Navigation("Mobiles");
                });

            modelBuilder.Entity("StoreSystem.Models.Mobile", b =>
                {
                    b.Navigation("SaleItems");
                });

            modelBuilder.Entity("StoreSystem.Models.Reports.Sales", b =>
                {
                    b.Navigation("SaleItems");
                });
#pragma warning restore 612, 618
        }
    }
}
