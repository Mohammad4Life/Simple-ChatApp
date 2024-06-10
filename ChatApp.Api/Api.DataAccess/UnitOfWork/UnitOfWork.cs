using Api.DataAccess.Context;
using Api.DataAccess.DomainRepository;
using Api.DataAccess.Models.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IApplicationDbContext ApplicationDbContext { get; }
    IApplicationUserRepository ApplicationUserRepository { get; }
    IContactRepository ContactRepository { get; }
    IConversationRepository ConversationRepository { get; }
    IMessageRepository MessageRepository { get; }
    IProfilePhotoRepository ProfilePhotoRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IProfilePhotoRepository _profilePhotoRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    public UnitOfWork(IApplicationUserRepository applicationUserRepository, IContactRepository contactRepository, IConversationRepository conversationRepository,
        IMessageRepository messageRepository, IProfilePhotoRepository profilePhotoRepository, IRefreshTokenRepository refreshTokenRepository, ApplicationDbContext context)
    {
        _applicationUserRepository = applicationUserRepository;
        _contactRepository = contactRepository;
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
        _profilePhotoRepository = profilePhotoRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _context = context;
    }

    public IApplicationDbContext ApplicationDbContext => _context;

    public IApplicationUserRepository ApplicationUserRepository => _applicationUserRepository;

    public IContactRepository ContactRepository => _contactRepository;

    public IConversationRepository ConversationRepository => _conversationRepository;

    public IMessageRepository MessageRepository => _messageRepository;

    public IProfilePhotoRepository ProfilePhotoRepository => _profilePhotoRepository;

    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            var entries = _context.ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdateDate = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).UpdateUser = await _applicationUserRepository.GetCurrentUserName();

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).InsertDate = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).InsertUser = await _applicationUserRepository.GetCurrentUserName();
                }
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}