using Api.DataAccess.Context;
using Api.DataAccess.GenericRepository;
using Api.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace Api.DataAccess.DomainRepository;

public interface IContactRepository : IRepository<Contact>
{

}
public class ContactRepository : Repository<Contact>, IContactRepository
{
    private readonly ApplicationDbContext _context;
    public ContactRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
        _context = context;
    }
}
