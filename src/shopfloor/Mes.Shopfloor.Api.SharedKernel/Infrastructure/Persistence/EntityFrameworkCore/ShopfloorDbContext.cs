using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.EntityFrameworkCore;

internal sealed class ShopfloorDbContext(IConfiguration _configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Mes.Shopfloor.Api.SharedKernel"));
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Mes.Shopfloor.Api.ProductionManagement"));
    }
}