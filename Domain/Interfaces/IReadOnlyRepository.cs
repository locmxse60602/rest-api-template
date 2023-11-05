using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IReadOnlyRepository<T> where T : class
{
    IQueryable<T> GetQueryable();

    IQueryable<T> GetFilterQueryable(Expression<Func<T, bool>> filter);

    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, Dictionary<string, string> sortProperties,
        int take, int skip);

    Task<T?> GetByIdAsync(string id);

    Task<T?> GetOnceAsync(Expression<Func<T, bool>> filter);
}
