using Api.DataAccess.Context;
using Api.DataAccess.GenericRepository;
using Api.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace Api.DataAccess.DomainRepository;

public interface IConversationRepository : IRepository<Conversation>
{

}

public class ConversationRepository : Repository<Conversation>, IConversationRepository
{
    private readonly ApplicationDbContext _context;
    public ConversationRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
        _context = context;
    }
}
