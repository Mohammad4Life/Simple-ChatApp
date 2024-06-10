using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Models.Abstraction;

public interface IBaseEntity
{
}

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public DateTime InsertDate { get; set; } = DateTime.Now;
    public DateTime? UpdateDate { get; set; }
    public string InsertUser { get; set; } = "System";
    public string UpdateUser { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}

public abstract class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
    }
}