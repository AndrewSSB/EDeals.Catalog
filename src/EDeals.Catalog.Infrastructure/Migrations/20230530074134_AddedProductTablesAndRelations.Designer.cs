﻿// <auto-generated />
using System;
using EDeals.Catalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EDeals.Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230530074134_AddedProductTablesAndRelations")]
    partial class AddedProductTablesAndRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("DiscountName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("DiscountPercent")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCode")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId1")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductId1");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MainImageId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductInventoryId")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("StockKeepingUnit")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId")
                        .IsUnique();

                    b.HasIndex("MainImageId")
                        .IsUnique();

                    b.HasIndex("ProductCategoryId");

                    b.HasIndex("ProductInventoryId")
                        .IsUnique();

                    b.HasIndex("SellerId")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("ProductsCategories");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.ProductInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<uint>("Quantity")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ProductsInventory");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Discount", b =>
                {
                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.Product", "Product")
                        .WithMany("Discounts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Image", b =>
                {
                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Product", b =>
                {
                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.Brand", "Brand")
                        .WithOne("Product")
                        .HasForeignKey("EDeals.Catalog.Domain.Entities.ItemEntities.Product", "BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.Image", "MainImage")
                        .WithOne()
                        .HasForeignKey("EDeals.Catalog.Domain.Entities.ItemEntities.Product", "MainImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.ProductInventory", "ProductInventory")
                        .WithOne("Product")
                        .HasForeignKey("EDeals.Catalog.Domain.Entities.ItemEntities.Product", "ProductInventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.Seller", "Seller")
                        .WithOne("Product")
                        .HasForeignKey("EDeals.Catalog.Domain.Entities.ItemEntities.Product", "SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("MainImage");

                    b.Navigation("ProductCategory");

                    b.Navigation("ProductInventory");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.ProductCategory", b =>
                {
                    b.HasOne("EDeals.Catalog.Domain.Entities.ItemEntities.ProductCategory", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Brand", b =>
                {
                    b.Navigation("Product")
                        .IsRequired();
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Product", b =>
                {
                    b.Navigation("Discounts");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.ProductCategory", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.ProductInventory", b =>
                {
                    b.Navigation("Product")
                        .IsRequired();
                });

            modelBuilder.Entity("EDeals.Catalog.Domain.Entities.ItemEntities.Seller", b =>
                {
                    b.Navigation("Product")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
