using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Infrastructure.EntityTypeConfigurations;

internal sealed class RequiredPartEntityTypeConfiguration : IEntityTypeConfiguration<RequiredPart>
{
    public void Configure(EntityTypeBuilder<RequiredPart> builder)
    {
        builder.ToTable("required_part", "pm_product_definition");
        builder.HasKey(p => new { p.ProductionStepId, p.PartId });
        builder.Property(p => p.ProductionStepId).HasColumnName("production_step_id").IsRequired();
        builder.Property(p => p.PartId).HasColumnName("part_id").IsRequired();
        builder.Property(p => p.Quantity).HasColumnName("quantity").IsRequired();
        builder.HasOne(p => p.Part).WithMany().HasForeignKey(f => f.PartId);
    }
}