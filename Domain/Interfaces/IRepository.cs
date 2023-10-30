namespace Domain.Interfaces;

public interface IRepository<T> : IReadOnlyRepository<T> where T : class
{
    Task<int> CreateAsync(IList<T> entities);

    Task<T> UpdateAsync(T entity);

    Task<int> BatchUpdateAsync(IList<T> entities);

    Task<int> DeleteAsync(T entity);

    Task<int> DeleteByIdAsync(string id);
}