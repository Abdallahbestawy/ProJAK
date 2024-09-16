using Microsoft.AspNetCore.Identity;
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

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
            (
                new IdentityRole
                {
                    Id = "4566b930-e1f9-4777-a945-7c4237260ed1",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "39527e49-3edc-4ed1-8e55-b7715292bd08",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
        }

    }
}
