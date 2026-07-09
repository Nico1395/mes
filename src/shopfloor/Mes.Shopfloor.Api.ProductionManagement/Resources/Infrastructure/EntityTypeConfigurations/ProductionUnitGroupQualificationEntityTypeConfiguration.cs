using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Infrastructure.EntityTypeConfigurations;

internal sealed class ProductionUnitGroupQualificationEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitGroupQualification>
{
    public void Configure(EntityTypeBuilder<ProductionUnitGroupQualification> builder)
    {
        builder.ToTable("production_unit_group_qualification", "resources");
        builder.HasKey(p => new { p.WorkerQualificationId, p.ProductionUnitGroupId });
        builder.Property(p => p.ProductionUnitGroupId).HasColumnName("production_unit_group_id").IsRequired();
        builder.Property(p => p.WorkerQualificationId).HasColumnName("worker_qualification_id").IsRequired();
    }
}