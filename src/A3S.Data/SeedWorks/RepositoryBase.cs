using A3S.Core.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace A3S.Data.SeedWorks
{
    public class RepositoryBase<T, Key> : IRepositoty<T, Key> where T : class
    {
        private readonly DbSet<T> _dbSet;
        protected readonly A3SContext _context;
        public RepositoryBase(A3SContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }
        public void Add(T entity)
        {
           _dbSet.Add(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        public void UpdateRange (IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Key id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<List<T>> FindOne(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(expression).ToListAsync();
        }

    }
}
