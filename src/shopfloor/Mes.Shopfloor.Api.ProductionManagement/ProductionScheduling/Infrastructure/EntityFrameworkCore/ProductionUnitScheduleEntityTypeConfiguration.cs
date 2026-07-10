using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Infrastructure.EntityFrameworkCore;

internal sealed class ProductionUnitScheduleEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitSchedule>
{
    public void Configure(EntityTypeBuilder<ProductionUnitSchedule> builder)
    {
        builder.ToTable("production_unit_schedule", "scheduling");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.ProductionUnitId).HasColumnName("production_unit_id").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(p => p.Tasks).WithOne().HasForeignKey(p => p.ProductionScheduleId);
    }
}