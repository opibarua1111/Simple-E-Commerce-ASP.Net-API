using Microsoft.EntityFrameworkCore;
using simple_ecommerce.api.Model;

namespace simple_ecommerce.api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        //public object Users { get; internal set; }
        //public object Products { get; internal set; }
    }
}
