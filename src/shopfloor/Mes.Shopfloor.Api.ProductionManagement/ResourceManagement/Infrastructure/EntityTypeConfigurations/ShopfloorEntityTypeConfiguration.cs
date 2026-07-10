using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class ShopfloorEntityTypeConfiguration : IEntityTypeConfiguration<Shopfloor>
{
    public void Configure(EntityTypeBuilder<Shopfloor> builder)
    {
        builder.ToTable("shopfloor", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.ManufacturingPlantId).HasColumnName("manufacturing_plant_id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(p => p.ProductionUnits).WithOne().HasForeignKey(p => p.ShopfloorId);
        builder.HasMany(p => p.ProductionLines).WithOne().HasForeignKey(p => p.ShopfloorId);
    }
}