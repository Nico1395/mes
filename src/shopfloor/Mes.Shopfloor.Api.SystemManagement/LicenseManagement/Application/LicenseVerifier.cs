using Mes.Shopfloor.Api.SharedKernel.Application.Licensing;

namespace Mes.Shopfloor.Api.SystemManagement.LicenseManagement.Application;

internal sealed class LicenseVerifier : ILicenseVerifier
{
    public Task<Dictionary<string, bool>> VerifyAsync(IEnumerable<string> licenseKeys, CancellationToken cancellationToken)
    {
        return Task.FromResult(licenseKeys.ToDictionary(l => l, _ => true));
    }
}