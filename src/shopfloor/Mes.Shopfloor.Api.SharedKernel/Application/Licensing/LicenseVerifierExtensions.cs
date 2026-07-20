namespace Mes.Shopfloor.Api.SharedKernel.Application.Licensing;

public static class LicenseVerifierExtensions
{
    public static async Task<bool> VerifyAsync(this ILicenseVerifier verifier, string licenseKey, CancellationToken cancellationToken)
    {
        var results = await verifier.VerifyAsync([licenseKey], cancellationToken);
        return results.Count > 0 && results[licenseKey];
    }
}