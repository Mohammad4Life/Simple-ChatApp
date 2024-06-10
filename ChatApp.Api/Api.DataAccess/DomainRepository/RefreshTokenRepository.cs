using Api.DataAccess.Context;
using Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.DataAccess.DomainRepository;

public interface IRefreshTokenRepository
{
    Task<Guid> AddRefreshToken(string UserId);
    Task<RefreshToken> Get(string UserId);
    Task<RefreshToken> GetAsync(string UserId);
    Task<bool> ValidateRefresh(string UserId, Guid RT);

}

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    public RefreshTokenRepository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<Guid> AddRefreshToken(string UserId)
    {
        var refreshToken = await Get(UserId);
        if (refreshToken != null)
        {
            _context.Set<RefreshToken>().Remove(refreshToken);
        }
        var model = await _context.Set<RefreshToken>().AddAsync(new RefreshToken { UserId = UserId });
        _context.SaveChanges();
        refreshToken = await Get(UserId);
        return refreshToken.RT;
    }

    public async Task<RefreshToken> Get(string UserId)
    {
        return _context.Set<RefreshToken>().AsNoTracking().FirstOrDefault(x => x.UserId == UserId);
    }

    public async Task<RefreshToken> GetAsync(string UserId)
    {
        return _context.Set<RefreshToken>().FirstOrDefault(x => x.UserId == UserId);
    }

    public async Task<bool> ValidateRefresh(string UserId, Guid RT)
    {
        return _context.Set<RefreshToken>().Any(x => x.UserId == UserId && x.RT == RT);
    }

}
