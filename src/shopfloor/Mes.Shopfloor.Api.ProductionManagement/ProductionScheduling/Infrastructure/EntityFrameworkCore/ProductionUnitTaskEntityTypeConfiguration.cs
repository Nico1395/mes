using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Infrastructure.EntityFrameworkCore;

internal sealed class ProductionUnitTaskEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitTask>
{
    public void Configure(EntityTypeBuilder<ProductionUnitTask> builder)
    {
        builder.ToTable("production_unit_task", "production_scheduling");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.ProductionUnitScheduleId).HasColumnName("production_unit_schedule_id").IsRequired();
        builder.Property(p => p.ScheduledProductionOrderTaskId).HasColumnName("scheduled_production_order_task_id").IsRequired();
        builder.Property(p => p.StartingAt).HasColumnName("starting_at").IsRequired();
        builder.Property(p => p.CompletingAt).HasColumnName("completing_at").IsRequired();
    }
}