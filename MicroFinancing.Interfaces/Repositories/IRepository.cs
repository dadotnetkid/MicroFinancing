using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    DbSet<T> Entity { get; }
    IQueryable<T> Fetch(Expression<Func<T, bool>> filterExpression);
    Task<T> AddAsync(T entity);
    Task<bool> AddAsync(IEnumerable<T>? items);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> filterExpression);
    Task<bool> DeleteAsync(IEnumerable<T> items);
    Task<bool> DeleteAsync(object? id);
}