using Domain;
using Domain.ProductParts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class ECommerceContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        

        public ECommerceContext() { }

        public ECommerceContext(DbContextOptions options) : base(options) { }
    }
}