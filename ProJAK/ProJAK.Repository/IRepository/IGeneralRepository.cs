using System.Linq.Expressions;

namespace ProJAK.Repository.IRepository
{
    public interface IGeneralRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid Id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetEntityByPropertyAsync(Func<T, bool> predicate);
        Task<IQueryable<T>> FindAllByForeignKeyAsync<TProperty>(Expression<Func<T, TProperty>> foreignKeySelector, TProperty foreignKey);
        Task<IEnumerable<T>> GetEntityByPropertyWithIncludeAsync(Func<T, bool> attributeSelector, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindWithIncludeIEnumerableAsync(params Expression<Func<T, object>>[] includes);


    }
}
