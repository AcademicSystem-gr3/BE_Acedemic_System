using System.Linq.Expressions;

namespace A3S.Core.SeedWorks
{
    public interface IRepositoty<T,Key> where T : class
    {
        Task<T> GetByIdAsync(Key id);

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> Find(Expression<Func<T,bool>> expression);

        Task<List<T>> FindOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Update (T entity);
        void AddRange(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
