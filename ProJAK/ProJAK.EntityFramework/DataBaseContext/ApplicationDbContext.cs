using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProJAK.Domain.Entities;

namespace ProJAK.EntityFramework.DataBaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<GraphicsCard> GraphicsCards { get; set; }
        public DbSet<Hard> Hards { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<ProductCart> ProductCarts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Screen> Screens { get; set; }
    }
}
