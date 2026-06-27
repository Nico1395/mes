using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Infrastructure;

internal sealed class ProductionStepEquipmentEntityTypeConfiguration : IEntityTypeConfiguration<ProductionStepEquipment>
{
    public void Configure(EntityTypeBuilder<ProductionStepEquipment> builder)
    {
        builder.ToTable("production_step_equipment", "product_definition");
        builder.HasKey(p => new { p.ProductionStepId, p.EquipmentId });
        builder.Property(p => p.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(p => p.EquipmentId).HasColumnName("equipment_id").IsRequired();
        builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
        builder.HasOne(p => p.Equipment).WithMany().HasForeignKey(f => f.EquipmentId);
    }
}