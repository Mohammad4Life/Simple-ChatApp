using Api.DataAccess.Context;
using Api.DataAccess.Models.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Api.DataAccess.GenericRepository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    public Repository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<bool> CheckDuplicate(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().CountAsync(predicate);
    }

    public async Task Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach(TEntity entity in entities)
        {
            entity.IsDeleted = true;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync().ConfigureAwait(false);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity> Get(int Id)
    {
        return await _context.Set<TEntity>().FindAsync(Id).ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync().ConfigureAwait(false);
    }

    public void RemovePermanent(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRangePermanent(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public async Task<TEntity> SafeGet(int Id)
    {
        return await _context.Set<TEntity>().Where(x => !x.IsDeleted).FirstAsync(x => x.Id == Id).ConfigureAwait(false);
    }
}
