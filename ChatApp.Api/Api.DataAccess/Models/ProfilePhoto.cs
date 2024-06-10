using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Models;

public class ProfilePhoto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime InsertDate { get; set; } = DateTime.Now;
    
    //Relations
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

public class ProfilePhotoConfigurations : IEntityTypeConfiguration<ProfilePhoto>
{
    public void Configure(EntityTypeBuilder<ProfilePhoto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(x => x.ProfilePhotos)
            .HasForeignKey(x => x.UserId);
    }
}