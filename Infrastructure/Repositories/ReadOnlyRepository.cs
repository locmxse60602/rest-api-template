using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
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

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, Dictionary<string, string> sortProperties,
        int take, int skip)
    {
        var queryable = GetFilterQueryable(filter);
        return await queryable.ToListAsync();
    }

    public Task<T> GetAsync(string id)
    {
        throw new NotImplementedException();
    }
}