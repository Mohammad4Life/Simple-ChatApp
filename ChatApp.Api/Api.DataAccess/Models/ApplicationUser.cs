using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public virtual string FullName => FirstName + " " + LastName; 
    public string EnFirstName { get; set; } = string.Empty;
    public string EnLastName { get; set; } = string.Empty;
    public virtual string EnFullName => EnFirstName + " " + EnLastName;
    public DateTime BirthDate { get; set; } = new DateTime(1998, 01, 01);
    public string InsertUser { get; set; } = "System";
    public string UpdateUser { get; set; } = string.Empty; 
    public bool IsDeleted { get; set; } = false;
    public RefreshToken? RT { get; set; }
    public string VerificationCode { get; set; } = string.Empty;

    //Relations
    public List<ProfilePhoto>? ProfilePhotos { get; set; }
    public List<Contact>? Contacts { get; set; }
    public List<Message>? Messages { get; set; }
    public List<Conversation>? Conversations { get; set; }
}

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

    }
}