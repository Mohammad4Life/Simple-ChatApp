using Api.DataAccess.Models.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Models;

public class Conversation : BaseEntity
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string RecieverId { get; set; }
    public ApplicationUser Reciever { get; set; }

    public ICollection<Message>? Messages { get; set; }
}

public class ConversationConfigurations : BaseEntityConfigurations<Conversation>
{
    public override void Configure(EntityTypeBuilder<Conversation> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Conversations)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Reciever)
            .WithMany(x => x.Conversations)
            .HasForeignKey(x => x.RecieverId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}