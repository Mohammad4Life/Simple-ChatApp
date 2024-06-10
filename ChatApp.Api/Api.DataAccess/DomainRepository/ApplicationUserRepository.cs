using Api.DataAccess.Context;
using Api.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.DataAccess.DomainRepository;

public interface IApplicationUserRepository
{
    Task<ApplicationUser> Get(string Id);
    Task<ApplicationUser> GetCurrentUser();
    Task<ApplicationUser> GetByUserName(string UserName);
    Task<ApplicationUser> GetByPhoneNumber(string PhoneNumber);
    Task<string> GetCurrentUserName();
    Task<string> AddAsync(ApplicationUser user);
    Task<bool> Delete(ApplicationUser user);
    Task<string> UpdateVerificationCode(string PhoneNumber);
    Task<bool> CheckDuplicate(Expression<Func<ApplicationUser, bool>> Predicate);
}
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContext;
    public ApplicationUserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext)
    {
        _context = context;
        _userManager = userManager;
        _httpContext = httpContext;
    }

    public async Task<string> AddAsync(ApplicationUser user)
    {
        await _userManager.CreateAsync(user);
        //await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> CheckDuplicate(Expression<Func<ApplicationUser, bool>> Predicate)
    {
        return await _context.ApplicationUsers.AnyAsync(Predicate);
    }

    public async Task<bool> Delete(ApplicationUser user)
    {
        user.IsDeleted = true;
        return true;
    }

    public async Task<ApplicationUser> Get(string Id)
    {
        return await _context.ApplicationUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<ApplicationUser> GetByPhoneNumber(string PhoneNumber)
    {
        return await _context.ApplicationUsers.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber);
    }

    public async Task<ApplicationUser> GetByUserName(string UserName)
    {
        return await _context.ApplicationUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == UserName);
    }

    public async Task<ApplicationUser> GetCurrentUser()
    {
        var claims = _httpContext.HttpContext.User.Claims.ToList();
        
        if(claims.Any(x => x.Type == "UserId"))
            throw new UnauthorizedAccessException();

        var userId = claims.FirstOrDefault(x => x.Type == "UserId").Value;

        return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<string> GetCurrentUserName()
    {
        var claims = _httpContext.HttpContext.User.Claims.ToList();

        if(claims.Count > 0)
        {
            var userId = claims.FirstOrDefault(x => x.Type == "UserId").Value;

            var user = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return string.Empty;

            return user.UserName;
        }

        return string.Empty;
    }
    public Task<string> UpdateVerificationCode(string PhoneNumber)
    {
        throw new NotImplementedException();
    }
}