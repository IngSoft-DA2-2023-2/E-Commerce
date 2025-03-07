﻿using Domain;
using Domain.PaymentMethodCategories;
using Domain.ProductParts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Context
{
    [ExcludeFromCodeCoverage]
    public class ECommerceContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Colour> Colours { get; set; }
        public virtual DbSet<StringWrapper> StringListWrappers { get; set; }

        public ECommerceContext() { }

        public ECommerceContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Paganza>();
            modelBuilder.Entity<Paypal>();
            modelBuilder.Entity<BankDebit>();
            modelBuilder.Entity<CreditCard>();

        }

    }
}