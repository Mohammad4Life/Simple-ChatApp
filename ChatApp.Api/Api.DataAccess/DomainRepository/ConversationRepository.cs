using Api.DataAccess.Context;
using Api.DataAccess.GenericRepository;
using Api.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace Api.DataAccess.DomainRepository;

public interface IConversationRepository : IRepository<Conversation>
{
    bool Exists(string UserId, string RecieverId);
}

public class ConversationRepository : Repository<Conversation>, IConversationRepository
{
    private readonly ApplicationDbContext _context;
    public ConversationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
        _context = context;
    }

    public bool Exists(string UserId, string RecieverId)
    {
        if (_context.Conversations.Any(x => x.UserId == UserId && x.RecieverId == RecieverId))
            return true;
        else 
            return false;
    }
}
