using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class, IEntityBase, new()
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ReadOnlyRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public IQueryable<T> GetQueryable()
    {
        return _applicationDbContext.Set<T>().AsQueryable();
    }

    public IQueryable<T> GetFilterQueryable(Expression<Func<T, bool>> filter)
    {
        return _applicationDbContext.Set<T>().AsQueryable().Where(filter);
    }

    public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, Dictionary<string, string> sortProperties, int take, int skip)
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetByIdAsync(string id)
    {
        Expression<Func<T, bool>> filter = x => x.Id.Equals(id);
        return GetFilterQueryable(filter).FirstOrDefaultAsync();
    }

    public Task<T?> GetOnceAsync(Expression<Func<T, bool>> filter)
    {
        return GetFilterQueryable(filter).FirstOrDefaultAsync();
    }
}
