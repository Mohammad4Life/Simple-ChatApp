using System.Linq.Expressions;

namespace Api.DataAccess.GenericRepository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> Get(int Id);
    Task<TEntity> SafeGet(int Id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    Task<int> AddAsync(TEntity entity);
    Task AddRange(IEnumerable<TEntity> entities);
    void RemovePermanent(TEntity entity);
    void RemoveRangePermanent(IEnumerable<TEntity> entities);
    Task Delete(TEntity entity);
    Task DeleteRange(IEnumerable<TEntity> entities);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> CheckDuplicate(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
}
