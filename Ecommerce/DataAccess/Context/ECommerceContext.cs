using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class ECommerceContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ECommerceContext() { }

        public ECommerceContext(DbContextOptions options) : base(options) { }
    }
}