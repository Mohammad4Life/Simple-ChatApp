using Api.DataAccess.Context;
using Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.DataAccess.DomainRepository;

public interface IProfilePhotoRepository
{
    Task<IEnumerable<ProfilePhoto>> GetAll();
    Task<ProfilePhoto> Get(Guid Id);
    Task<Guid> AddAsync(ProfilePhoto profilePhoto);
    Task Delete(ProfilePhoto profilePhoto);
}

public class ProfilePhotoRepository : IProfilePhotoRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    public ProfilePhotoRepository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(ProfilePhoto profilePhoto)
    {
        await _context.ProfilePhotos.AddAsync(profilePhoto);
        await _context.SaveChangesAsync();
        return profilePhoto.Id;
    }

    public async Task Delete(ProfilePhoto profilePhoto)
    {
        _context.ProfilePhotos.Remove(profilePhoto);
    }

    public async Task<ProfilePhoto> Get(Guid Id)
    {
        return await _context.ProfilePhotos.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<IEnumerable<ProfilePhoto>> GetAll()
    {
        return await _context.ProfilePhotos.ToListAsync();
    }
}
