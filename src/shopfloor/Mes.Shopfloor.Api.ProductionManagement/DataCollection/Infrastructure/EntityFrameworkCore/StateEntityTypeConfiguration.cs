using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Infrastructure.EntityFrameworkCore;

internal sealed class StateEntityTypeConfiguration : IEntityTypeConfiguration<ProductionUnitState>
{
    public void Configure(EntityTypeBuilder<ProductionUnitState> builder)
    {
        builder.ToTable("state", "data_collection");
        builder.HasIndex(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        builder.Property(d => d.StateGroupId).HasColumnName("state_group_id").IsRequired();
        builder.Property(d => d.Order).HasColumnName("order").IsRequired();
        builder.Property(d => d.Code).HasColumnName("code").HasMaxLength(32).IsRequired();
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").HasMaxLength(256);
        builder.Property(d => d.Color).HasColumnName("color").HasMaxLength(32).IsRequired();

        builder.HasData(
            new()
            {
                Order = 1,
                Name = "Too small",
                Code = "A",
                Color = "#FFCC00",
                StateGroupId = Guid.Parse("4d93e894-2809-458a-b685-a117594a6d61"),
            },
            new()
            {
                Order = 2,
                Name = "Cracked",
                Code = "B",
                Color = "#3300FF",
                StateGroupId = Guid.Parse("4d93e894-2809-458a-b685-a117594a6d61"),
            });
    }
}