﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ECommerceContext))]
    [Migration("20231108165447_v3_productInclude")]
    [ExcludeFromCodeCoverage]
    partial class v3_productInclude
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PaymentMethod");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IncludeForPromotion")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<Guid?>("PurchaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.ProductParts.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Domain.ProductParts.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.ProductParts.Colour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Colours");
                });

            modelBuilder.Entity("Domain.ProductParts.StringWrapper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("StringListWrappers");
                });

            modelBuilder.Entity("Domain.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurrentPromotion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PaymentMethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("UserId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("Domain.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.PaymentMethodCategories.BankDebit", b =>
                {
                    b.HasBaseType("Domain.PaymentMethod");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("BankDebit");
                });

            modelBuilder.Entity("Domain.PaymentMethodCategories.CreditCard", b =>
                {
                    b.HasBaseType("Domain.PaymentMethod");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("CreditCard");
                });

            modelBuilder.Entity("Domain.PaymentMethodCategories.Paganza", b =>
                {
                    b.HasBaseType("Domain.PaymentMethod");

                    b.HasDiscriminator().HasValue("Paganza");
                });

            modelBuilder.Entity("Domain.PaymentMethodCategories.Paypal", b =>
                {
                    b.HasBaseType("Domain.PaymentMethod");

                    b.HasDiscriminator().HasValue("Paypal");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.HasOne("Domain.ProductParts.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.ProductParts.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Purchase", null)
                        .WithMany("Cart")
                        .HasForeignKey("PurchaseId");

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.ProductParts.Colour", b =>
                {
                    b.HasOne("Domain.Product", null)
                        .WithMany("Colours")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Domain.ProductParts.StringWrapper", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Domain.Purchase", b =>
                {
                    b.HasOne("Domain.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Session", b =>
                {
                    b.HasOne("Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Navigation("Colours");
                });

            modelBuilder.Entity("Domain.Purchase", b =>
                {
                    b.Navigation("Cart");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
