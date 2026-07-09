using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Infrastructure.EntityTypeConfigurations;

internal sealed class ManufacturingPlantEntityTypeConfiguration : IEntityTypeConfiguration<ManufacturingPlant>
{
    public void Configure(EntityTypeBuilder<ManufacturingPlant> builder)
    {
        builder.ToTable("manufacturing_plant", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(p => p.Shopfloors).WithOne().HasForeignKey(p => p.ManufacturingPlantId);
    }
}