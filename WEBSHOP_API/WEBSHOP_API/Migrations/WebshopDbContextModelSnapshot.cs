﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEBSHOP_API.Models;

#nullable disable

namespace WEBSHOP_API.Migrations
{
    [DbContext(typeof(WebshopDbContext))]
    partial class WebshopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("WEBSHOP_API.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountFirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountNameTitles")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccountPassword")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.HasKey("AccountId");

                    b.HasIndex("CartId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WEBSHOP_API.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductsCounts")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductsName")
                        .HasColumnType("TEXT");

                    b.HasKey("CartId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("WEBSHOP_API.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsProductOnSale")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsProductPromoted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductBasePrice")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductCategory")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductImage")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WEBSHOP_API.Models.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductStocks")
                        .HasColumnType("INTEGER");

                    b.HasKey("StockId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("WEBSHOP_API.Storage.StorageLogger", b =>
                {
                    b.Property<int>("StorageLoggerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<int>("StockChange")
                        .HasColumnType("INTEGER");

                    b.HasKey("StorageLoggerId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("WEBSHOP_API.Models.Account", b =>
                {
                    b.HasOne("WEBSHOP_API.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId");

                    b.Navigation("Cart");
                });
#pragma warning restore 612, 618
        }
    }
}
