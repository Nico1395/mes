using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class RequiredMaterialEntityTypeConfiguration : IEntityTypeConfiguration<RequiredMaterial>
{
    public void Configure(EntityTypeBuilder<RequiredMaterial> builder)
    {
        builder.ToTable("required_material", "pm_product_definition");
        builder.HasKey(p => new { p.ProductionStepId, p.MaterialId });
        builder.Property(p => p.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(p => p.MaterialId).HasColumnName("material_id").IsRequired();
        builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
        builder.HasOne(p => p.Material).WithMany().HasForeignKey(f => f.MaterialId);
    }
}