using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionStepParameterEntityTypeConfiguration : IEntityTypeConfiguration<ProductionStepParameter>
{
    public void Configure(EntityTypeBuilder<ProductionStepParameter> builder)
    {
        builder.ToTable("production_step_parameter", "pm_product_definition");
        builder.HasKey(s => new { s.ProductionStepId, s.Key });
        builder.Property(s => s.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(s => s.Key).HasColumnName("key").HasMaxLength(128).IsRequired();
        builder.Property(s => s.Value).HasColumnName("value").HasMaxLength(1024).IsRequired();
        builder.Property(s => s.Type).HasColumnName("type").IsRequired();
    }
}