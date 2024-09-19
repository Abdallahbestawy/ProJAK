using Microsoft.EntityFrameworkCore;
using ProJAK.EntityFramework.DataBaseContext;
using ProJAK.Repository.IRepository;
using System.Linq.Expressions;

namespace ProJAK.Repository.Repository
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        #region fields
        protected ApplicationDbContext _context;
        #endregion

        #region ctor
        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Add
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        #endregion

        #region AddRange

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        #endregion

        #region DeleteRange
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        #endregion

        #region FindAllByForeignKey
        public async Task<IQueryable<T>> FindAllByForeignKeyAsync<TProperty>(Expression<Func<T, TProperty>> foreignKeySelector, TProperty foreignKey)
        {
            var parameter = foreignKeySelector.Parameters.Single();
            var body = Expression.Equal(foreignKeySelector.Body, Expression.Constant(foreignKey));

            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            var entities = await _context.Set<T>().Where(predicate).AsQueryable().ToListAsync();
            return entities.AsQueryable();
        }
        #endregion

        #region GetAll

        public async Task<IQueryable<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities.AsQueryable();
        }
        #endregion

        #region GetById
        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        #endregion

        #region GetEntityByProperty
        public async Task<IEnumerable<T>> GetEntityByPropertyAsync(Func<T, bool> predicate)
        {
            var entities = _context.Set<T>().Where(predicate).ToList();
            return entities;
        }
        #endregion

        #region GetEntityByPropertyWithInclude
        public async Task<IEnumerable<T>> GetEntityByPropertyWithIncludeAsync(Func<T, bool> attributeSelector, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var filteredEntities = query.Where(attributeSelector).ToList();

            return filteredEntities;
        }
        #endregion

        #region Update

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = _context.Set<T>().Update(entity);
            return entry.Entity;
        }
        #endregion

        #region UpdateRange
        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
        #endregion

        #region FindWithIncludeIEnumerable
        public async Task<IEnumerable<T>> FindWithIncludeIEnumerableAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> entities = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }
            }

            return await entities.ToListAsync();
        }
        #endregion
    }
}
