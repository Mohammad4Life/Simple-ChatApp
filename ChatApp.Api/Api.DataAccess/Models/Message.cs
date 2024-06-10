using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Models;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime SendDate { get; set; } = DateTime.Now;
    public bool SelfDelete { get; set; } = false;
    public bool FullDelete { get; set; } = false;

    //Relations
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    //This message is either a node of a reply set
    public ICollection<Message>? Children { get; set; }

    //Or a message replied to another message!
    //Or even both!
    public int? ParentId { get; set; }
    public virtual Message Parent { get; set; }
}

public class MessageConfigurations : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Conversation)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId);
    }
}