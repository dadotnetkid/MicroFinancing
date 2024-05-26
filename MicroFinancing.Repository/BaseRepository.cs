using System.Collections.Generic;
using System.Linq.Expressions;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Repositories
{
    public class BaseRepository<T, TKey> : IRepository<T, TKey> where T
        : class
    {
        private readonly MFDbContext _db;
        private readonly ICurrentUser _currentUser;

        public BaseRepository(MFDbContext db, ICurrentUser currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }
        public DbSet<T> Entity => _db.Set<T>();

        public IQueryable<T> Fetch(Expression<Func<T, bool>> filterExpression)
        {
            return Entity.Where(filterExpression);
        }
        public async Task<T> AddAsync(T entity)
        {
            if (typeof(BaseEntity<TKey>).IsAssignableFrom(typeof(T)))
            {
                var type = entity.GetType();
                type.GetProperty(nameof(BaseEntity<TKey>.CreatorUserId)).SetValue(entity, _currentUser.UserId);
            }

            Entity.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AddAsync(IEnumerable<T>? items)
        {
            if (items != null)
            {
                await Entity.AddRangeAsync(items);
                await _db.SaveChangesAsync();
                return true;
            }
            return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (Exists(entity))
            {
                _db.Entry(entity).State = EntityState.Detached;
            }

            Entity.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> filterExpression)
        {
            var list = Entity.Where(filterExpression);
            _db.RemoveRange(list);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> items)
        {
            _db.RemoveRange(items);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(object? id)
        {
            var entity = await Entity.FindAsync(id);
            if (entity == null) return false;
            _db.Remove<T>(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        private bool Exists(T entity)
        {
            return Entity.Local.Any(e => e == entity);
        }
    }
}