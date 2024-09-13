﻿using Microsoft.EntityFrameworkCore;
using ProJAK.EntityFramework.DataBaseContext;
using ProJAK.Repository.IRepository;
using System.Linq.Expressions;

namespace ProJAK.Repository.Repository
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<IQueryable<T>> FindAllByForeignKeyAsync<TProperty>(Expression<Func<T, TProperty>> foreignKeySelector, TProperty foreignKey)
        {
            var parameter = foreignKeySelector.Parameters.Single();
            var body = Expression.Equal(foreignKeySelector.Body, Expression.Constant(foreignKey));

            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            var entities = await _context.Set<T>().Where(predicate).AsQueryable().ToListAsync();
            return entities.AsQueryable();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities.AsQueryable();
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<IEnumerable<T>> GetEntityByPropertyAsync(Func<T, bool> predicate)
        {
            var entities = _context.Set<T>().Where(predicate).ToList();
            return entities;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = _context.Set<T>().Update(entity);
            return entry.Entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
    }
}
