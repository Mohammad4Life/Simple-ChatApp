using Api.DataAccess.Context;
using Api.DataAccess.GenericRepository;
using Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Api.DataAccess.DomainRepository;

public interface IContactRepository : IRepository<Contact>
{
    Task<bool> CheckDuplicate(Expression<Func<Contact, bool>> Predicate);

    Task<string> GetUserIdByContactId(int ContactId);
}
public class ContactRepository : Repository<Contact>, IContactRepository
{
    private readonly ApplicationDbContext _context;
    public ContactRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
        _context = context;
    }

    public async Task<bool> CheckDuplicate(Expression<Func<Contact, bool>> Predicate)
    {
        return await _context.Contacts.AnyAsync(Predicate);
    }

    public async Task<string> GetUserIdByContactId(int ContactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == ContactId);
        return contact.UserId;
    }
}