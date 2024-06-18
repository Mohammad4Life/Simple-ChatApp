using Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.DataAccess.Context;

public interface IApplicationDbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public IDbConnection Connection { get; }
}
