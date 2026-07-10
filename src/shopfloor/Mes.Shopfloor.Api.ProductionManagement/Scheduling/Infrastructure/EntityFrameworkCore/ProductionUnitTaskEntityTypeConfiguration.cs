using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Scheduling.Infrastructure.EntityFrameworkCore;

internal sealed class ProductionUnitTaskEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitTask>
{
    public void Configure(EntityTypeBuilder<ProductionUnitTask> builder)
    {
        builder.ToTable("production_unit_task", "scheduling");
        builder.HasKey(p => p.ProductionScheduleId);
        builder.Property(p => p.ProductionScheduleId).HasColumnName("production_schedule_Id").IsRequired();
        builder.Property(p => p.ProductionUnitId).HasColumnName("production_unit_id").IsRequired();
        builder.Property(p => p.ProductionOrderId).HasColumnName("production_order_id").IsRequired();
        builder.Property(p => p.StartingAt).HasColumnName("starting_at").IsRequired();
        builder.Property(p => p.CompletingAt).HasColumnName("completing_at").IsRequired();
        builder.HasOne(p => p.Order).WithOne().HasForeignKey<ProductionUnitTask>(p => p.ProductionOrderId);
    }
}