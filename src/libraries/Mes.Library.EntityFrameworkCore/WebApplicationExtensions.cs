using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.EntityFrameworkCore;

public static class WebApplicationExtensions
{
    public static void InitializeEfCoreIncludeCache(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DbContext>();

        IncludeResolver.CalculateIncludeStrings(context);
    }
}