using ProJAK.Domain.Entities;
using ProJAK.EntityFramework.DataBaseContext;
using ProJAK.Repository.IRepository;

namespace ProJAK.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGeneralRepository<Product> Products { get; private set; }
        public IGeneralRepository<Cart> Carts { get; private set; }
        public IGeneralRepository<Categorie> Categories { get; private set; }
        public IGeneralRepository<GraphicsCard> GraphicsCards { get; private set; }
        public IGeneralRepository<Hard> Hards { get; private set; }
        public IGeneralRepository<Image> Images { get; private set; }
        public IGeneralRepository<Manufacturer> Manufacturers { get; private set; }
        public IGeneralRepository<Order> Orders { get; private set; }
        public IGeneralRepository<OrderedProduct> OrderedProducts { get; private set; }
        public IGeneralRepository<Processor> Processors { get; private set; }
        public IGeneralRepository<ProductCart> ProductCarts { get; private set; }
        public IGeneralRepository<Review> Reviews { get; private set; }
        public IGeneralRepository<Screen> Screens { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new GeneralRepository<Product>(_context);
            Carts = new GeneralRepository<Cart>(_context);
            Categories = new GeneralRepository<Categorie>(_context);
            GraphicsCards = new GeneralRepository<GraphicsCard>(_context);
            Hards = new GeneralRepository<Hard>(_context);
            Images = new GeneralRepository<Image>(_context);
            Manufacturers = new GeneralRepository<Manufacturer>(_context);
            Orders = new GeneralRepository<Order>(_context);
            OrderedProducts = new GeneralRepository<OrderedProduct>(_context);
            ProductCarts = new GeneralRepository<ProductCart>(_context);
            Reviews = new GeneralRepository<Review>(_context);
            Screens = new GeneralRepository<Screen>(_context);
            Processors = new GeneralRepository<Processor>(_context);
        }
        public async Task<bool> SaveAsync()
        {
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
