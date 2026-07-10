using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionStepPartEntityTypeConfiguration : IEntityTypeConfiguration<ProductionStepPart>
{
    public void Configure(EntityTypeBuilder<ProductionStepPart> builder)
    {
        builder.ToTable("production_step_part", "pm_product_definition");
        builder.HasKey(p => new { p.ProductionStepId, p.PartId });
        builder.Property(p => p.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(p => p.PartId).HasColumnName("part_id").IsRequired();
        builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
        builder.HasOne(p => p.Part).WithMany().HasForeignKey(f => f.PartId);
    }
}