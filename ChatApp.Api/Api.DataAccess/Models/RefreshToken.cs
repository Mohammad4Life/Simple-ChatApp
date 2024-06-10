using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Models;

public class RefreshToken
{
    public string UserId { get; set; }
    public Guid RT { get; set; } = Guid.NewGuid();
    public ApplicationUser User { get; set; }
}

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public virtual void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RT });

        builder.HasOne(rt => rt.User)
            .WithOne(u => u.RT);
    }
}
