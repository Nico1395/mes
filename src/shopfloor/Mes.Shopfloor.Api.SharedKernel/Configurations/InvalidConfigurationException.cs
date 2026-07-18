using System.Diagnostics.CodeAnalysis;

namespace Mes.Shopfloor.Api.SharedKernel.Configurations;

public sealed class InvalidConfigurationException(string message) : Exception(message)
{
    public static void ThrowIfNull([NotNull] string? configurationKey)
    {
        if (string.IsNullOrWhiteSpace(configurationKey))
            throw new InvalidConfigurationException($"Configuration for key '{configurationKey}' not provided.");
    }
}