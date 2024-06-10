using Api.DataAccess.Context;
using Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.DataAccess.DomainRepository;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAllMessages(int ConversationId);
    Task<Message> GetMessage(int Id, int ConversationId);
    Task<int> AddAsync(Message message);
    Task SelfDelete(Message message);
    Task FullDelete(Message message);
}

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;
    public MessageRepository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
        return message.Id;
    }

    public async Task FullDelete(Message message)
    {
        message.FullDelete = true;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Message>> GetAllMessages(int ConversationId)
    {
        return await _context.Messages.Where(x => x.ConversationId == ConversationId).ToListAsync();
    }

    public async Task<Message> GetMessage(int Id, int ConversationId)
    {
        return await _context.Messages.Where(x => x.ConversationId == ConversationId).FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task SelfDelete(Message message)
    {
        message.SelfDelete = true;
        await _context.SaveChangesAsync();
    }
}
