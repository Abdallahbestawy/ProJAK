using ProJAK.Domain.Entities;

namespace ProJAK.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGeneralRepository<Product> Products { get; }
        IGeneralRepository<Cart> Carts { get; }
        IGeneralRepository<Categorie> Categories { get; }
        IGeneralRepository<GraphicsCard> GraphicsCards { get; }
        IGeneralRepository<Hard> Hards { get; }
        IGeneralRepository<Image> Images { get; }
        IGeneralRepository<Manufacturer> Manufacturers { get; }
        IGeneralRepository<Order> Orders { get; }
        IGeneralRepository<OrderedProduct> OrderedProducts { get; }
        IGeneralRepository<Processor> Processors { get; }
        IGeneralRepository<ProductCart> ProductCarts { get; }
        IGeneralRepository<Review> Reviews { get; }
        IGeneralRepository<Screen> Screens { get; }
        Task<bool> SaveAsync();

    }
}
