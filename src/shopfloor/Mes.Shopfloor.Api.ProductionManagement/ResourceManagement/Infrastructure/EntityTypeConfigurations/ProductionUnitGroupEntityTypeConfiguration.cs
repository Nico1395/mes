using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionUnitGroupEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitGroup>
{
    public void Configure(EntityTypeBuilder<ProductionUnitGroup> builder)
    {
        builder.ToTable("production_unit_group", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256).IsRequired();
        builder.Property(p => p.StateGroupId).HasColumnName("state_group_id").IsRequired();
        builder.Property(p => p.RejectGroupId).HasColumnName("reject_group_id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(p => p.ProductionUnits).WithOne(p => p.Group).HasForeignKey(p => p.GroupId);
        builder.HasMany(p => p.RequiredQualifications).WithOne(p => p.ProductionUnitGroup).HasForeignKey(p => p.ProductionUnitGroupId);
    }
}