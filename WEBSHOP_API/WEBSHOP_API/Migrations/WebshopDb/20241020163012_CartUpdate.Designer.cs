﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEBSHOP_API.Database;

#nullable disable

namespace WEBSHOP_API.Migrations.WebshopDb
{
    [DbContext(typeof(WebshopDbContext))]
    [Migration("20241020163012_CartUpdate")]
    partial class CartUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("WEBSHOP_API.Models.Cart", b =>
                {
                    b.Property<string>("CartId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductCount")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductIds")
                        .IsRequired()
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
                        .IsRequired()
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

            modelBuilder.Entity("WEBSHOP_API.Models.UserData", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserFirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserLastPurchaseCategory")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserNameTitles")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("UserDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
