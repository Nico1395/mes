using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Infrastructure.EntityFrameworkCore;

internal sealed class RejectGroupEntityTypeConfiguration : IEntityTypeConfiguration<RejectGroup>
{
    public void Configure(EntityTypeBuilder<RejectGroup> builder)
    {
        builder.ToTable("reject_group", "data_collection");
        builder.HasIndex(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(d => d.CreatedAt).HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt).HasColumnName("updated_at");
        builder.HasMany(d => d.Rejects).WithOne(d => d.RejectGroup).HasForeignKey(d => d.RejectGroupId);

        builder.HasData(new RejectGroup()
        {
            Id = Guid.Parse("69ea30ac-8a9b-499f-85b4-6e064c98400a"),
            Name = "Extruders A",
        });
    }
}