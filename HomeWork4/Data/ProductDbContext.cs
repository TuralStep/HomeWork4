using HomeWork4.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeWork4.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options)
            : base(options) {}

        public DbSet<Product> Products { get; set; }

    }
}
