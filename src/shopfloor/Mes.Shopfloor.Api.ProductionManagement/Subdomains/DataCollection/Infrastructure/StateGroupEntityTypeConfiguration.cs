using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Infrastructure;

internal sealed class StateGroupEntityTypeConfiguration : IEntityTypeConfiguration<StateGroup>
{
    public void Configure(EntityTypeBuilder<StateGroup> builder)
    {
        builder.ToTable("state_group", "data_collection");
        builder.HasIndex(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(d => d.CreatedAt).HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt).HasColumnName("updated_at");
        builder.HasMany(d => d.States).WithOne(d => d.StateGroup).HasForeignKey(d => d.StateGroupId);
        
        builder.HasData(new RejectGroup()
        {
            Id = Guid.Parse("4d93e894-2809-458a-b685-a117594a6d61"),
            Name = "Extruders A",
        });
    }
}