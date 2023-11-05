using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IReadOnlyRepository<T> where T : class
{
    IQueryable<T> GetQueryable();

    IQueryable<T> GetFilterQueryable(Expression<Func<T, bool>> filter);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, Dictionary<string, string> sortProperties,
        int take, int skip);

    Task<T> GetAsync(string id);
}
