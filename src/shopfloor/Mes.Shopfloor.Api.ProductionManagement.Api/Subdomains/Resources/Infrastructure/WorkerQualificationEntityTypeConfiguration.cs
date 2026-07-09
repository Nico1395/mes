using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Infrastructure;

internal sealed class WorkerQualificationEntityTypeConfiguration : IEntityTypeConfiguration<WorkerQualification>
{
    public void Configure(EntityTypeBuilder<WorkerQualification> builder)
    {
        builder.ToTable("worker_qualification", "resources");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        builder.HasMany(p => p.WorkerGroups).WithOne(p => p.WorkerQualification).HasForeignKey(p => p.WorkerQualificationId);
        builder.HasMany(p => p.ProductionUnitGroups).WithOne(p => p.WorkerQualification).HasForeignKey(p => p.WorkerQualificationId);
    }
}