using Api.DataAccess.Models.Abstraction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Models;

public class Contact : BaseEntity
{
    public string ContactName { get; set; } = string.Empty;
    public string ContactLastName { get; set; }  = string.Empty;
    public string ContactPhoneNumber { get; set; } = string.Empty;

    //Relations
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Conversation>? Conversations { get; set; }
}

public class ContactConfigurations : BaseEntityConfigurations<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Contacts)
            .HasForeignKey(x => x.UserId);
    }
}