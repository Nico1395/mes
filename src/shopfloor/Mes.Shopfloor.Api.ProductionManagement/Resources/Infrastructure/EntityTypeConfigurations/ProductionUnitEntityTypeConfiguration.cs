using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionUnitEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnit>
{
    public void Configure(EntityTypeBuilder<ProductionUnit> builder)
    {
        builder.ToTable("production_unit", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.ShopfloorId).HasColumnName("shopfloor_id");
        builder.Property(p => p.ProductionLineId).HasColumnName("production_line_id");
        builder.Property(p => p.Key).HasColumnName("key").HasMaxLength(64).IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.TypeId).HasColumnName("type_id").IsRequired();
        builder.Property(p => p.GroupId).HasColumnName("group_id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasOne(p => p.Type).WithMany().HasForeignKey(p => p.TypeId);
    }
}