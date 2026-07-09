using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Infrastructure.EntityTypeConfigurations;

internal sealed class WorkerGroupQualificationEntityTypeConfiguration : IEntityTypeConfiguration<WorkerGroupWorkerQualification>
{
    public void Configure(EntityTypeBuilder<WorkerGroupWorkerQualification> builder)
    {
        builder.ToTable("worker_group_qualification", "resources");
        builder.HasKey(p => new { p.WorkerQualificationId, p.WorkerGroupId });
        builder.Property(p => p.WorkerGroupId).HasColumnName("worker_group_id").IsRequired();
        builder.Property(p => p.WorkerQualificationId).HasColumnName("worker_qualification_id").IsRequired();
    }
}