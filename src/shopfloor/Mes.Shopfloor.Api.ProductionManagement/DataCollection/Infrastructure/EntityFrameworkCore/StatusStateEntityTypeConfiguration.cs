using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Infrastructure.EntityFrameworkCore;

internal sealed class StatusStateEntityTypeConfiguration : IEntityTypeConfiguration<StatusState>
{
    public void Configure(EntityTypeBuilder<StatusState> builder)
    {
        builder.ToTable("status_state", "data_collection");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.ProductionUnitId).HasColumnName("production_unit_id").IsRequired();
        builder.Property(d => d.StateId).HasColumnName("state_id").IsRequired();
        builder.Property(d => d.StartedAt).HasColumnName("started_at").IsRequired();
        builder.Property(d => d.EndedAt).HasColumnName("ended_at");
        builder.HasOne(s => s.State).WithMany().HasForeignKey(d => d.StateId);
    }
}