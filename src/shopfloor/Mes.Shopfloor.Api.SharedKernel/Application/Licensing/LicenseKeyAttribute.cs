using System.Collections.Concurrent;
using System.Reflection;

namespace Mes.Shopfloor.Api.SharedKernel.Application.Licensing;

[AttributeUsage(AttributeTargets.Class)]
public sealed class LicenseKeyAttribute(string key) : Attribute
{
    private static readonly ConcurrentDictionary<Type, string?> _typeLicenseKeys = [];

    public string Key { get; } = key;

    public static string? GetLicenseKey(Type type)
    {
        return _typeLicenseKeys.GetOrAdd(type, t => t.GetCustomAttribute<LicenseKeyAttribute>()?.Key);
    }
    
    public static string? GetLicenseKey<T>()
    {
        return GetLicenseKey(typeof(T));
    }
}