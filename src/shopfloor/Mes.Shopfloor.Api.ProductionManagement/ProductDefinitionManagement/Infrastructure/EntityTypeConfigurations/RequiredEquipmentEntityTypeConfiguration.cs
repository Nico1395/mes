using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class RequiredEquipmentEntityTypeConfiguration : IEntityTypeConfiguration<RequiredEquipment>
{
    public void Configure(EntityTypeBuilder<RequiredEquipment> builder)
    {
        builder.ToTable("required_equipment", "pm_product_definition");
        builder.HasKey(p => new { p.ProductionStepId, p.EquipmentId });
        builder.Property(p => p.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(p => p.EquipmentId).HasColumnName("equipment_id").IsRequired();
        builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
    }
}