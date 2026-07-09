using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionStepMaterialEntityTypeConfiguration : IEntityTypeConfiguration<ProductionStepMaterial>
{
    public void Configure(EntityTypeBuilder<ProductionStepMaterial> builder)
    {
        builder.ToTable("production_step_material", "pm_product_definition");
        builder.HasKey(p => new { p.ProductionStepId, p.MaterialId });
        builder.Property(p => p.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(p => p.MaterialId).HasColumnName("material_id").IsRequired();
        builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
        builder.HasOne(p => p.Material).WithMany().HasForeignKey(f => f.MaterialId);
    }
}