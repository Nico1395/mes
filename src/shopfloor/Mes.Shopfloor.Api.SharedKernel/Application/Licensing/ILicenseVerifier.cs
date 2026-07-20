namespace Mes.Shopfloor.Api.SharedKernel.Application.Licensing;

public interface ILicenseVerifier
{
    Task<Dictionary<string, bool>> VerifyAsync(IEnumerable<string> licenseKeys, CancellationToken cancellationToken);
}