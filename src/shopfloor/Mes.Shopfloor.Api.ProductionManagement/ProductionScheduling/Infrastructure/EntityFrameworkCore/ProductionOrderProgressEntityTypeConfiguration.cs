using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Infrastructure.EntityFrameworkCore;

internal sealed class ProductionOrderProgressEntityTypeConfiguration : IEntityTypeConfiguration<ProductionOrderProgress>
{
    public void Configure(EntityTypeBuilder<ProductionOrderProgress> builder)
    {
        builder.ToTable("production_order_progress", "scheduling");
        builder.HasKey(p => p.ProductionOrderId);
        builder.Property(p => p.ProductionOrderId).HasColumnName("production_order_id").IsRequired();
        builder.Property(p => p.TargetQuantity).HasColumnName("target_quantity").IsRequired();
        builder.Property(p => p.ProducedQuantity).HasColumnName("produced_quantity");
        builder.Property(p => p.TargetDate).HasColumnName("target_date").IsRequired();
        builder.Property(p => p.ProductionProcessId).HasColumnName("production_process_id");
        builder.Property(p => p.ProductionProcessStepId).HasColumnName("production_process_step_id");
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
    }
}