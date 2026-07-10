namespace Mes.Shopfloor.Client.SharedKernel.Http;

public static class HttpClientFactoryExtensions
{
    public static HttpClient CreateApiClient(this IHttpClientFactory factory)
    {
        return factory.CreateClient(HttpClientConstants.ApiHttpClientName);
    }
}