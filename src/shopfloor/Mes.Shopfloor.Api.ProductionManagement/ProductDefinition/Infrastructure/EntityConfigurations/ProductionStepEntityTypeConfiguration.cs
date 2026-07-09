using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Infrastructure.EntityConfigurations;

internal sealed class ProductionStepEntityTypeConfiguration : IEntityTypeConfiguration<ProductionStep>
{
    public void Configure(EntityTypeBuilder<ProductionStep> builder)
    {
        builder.ToTable("production_step", "pm_product_definition");
        builder.HasIndex(x => x.Id);
        builder.Property(s => s.Id).HasColumnName("id").IsRequired();
        builder.Property(s => s.ProductionProcessId).HasColumnName("production_process_id").IsRequired();
        builder.Property(s => s.Index).HasColumnName("index").IsRequired();
        builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(s => s.Description).HasColumnName("description").HasMaxLength(256);
        builder.ComplexProperty(s => s.Duration, d =>
        {
            d.Property(p => p.Value).HasColumnName("duration_value").IsRequired();
            d.Property(p => p.DeviationSeconds).HasColumnName("duration_deviation_seconds").IsRequired();
        });
        builder.Property(s => s.ProductionUnitGroupId).HasColumnName("production_unit_group_id").IsRequired();
        builder.HasMany(p => p.Parameters).WithOne().HasForeignKey(p => p.ProductionStepId);
        builder.HasMany(p => p.Parts).WithOne(p => p.ProductionStep).HasForeignKey(f => f.ProductionStepId);
        builder.HasMany(p => p.Material).WithOne(p => p.ProductionStep).HasForeignKey(f => f.ProductionStepId);
        builder.HasMany(p => p.Equipment).WithOne(p => p.ProductionStep).HasForeignKey(f => f.ProductionStepId);
    }
}