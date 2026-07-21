using Urleso.Web.Api.ShortenedUrls;

namespace Urleso.Web.Api;

internal static class DependencyInjection
{
    /// <param name="baseAddress">The app's own origin; the service proxies "api/*" to the Urleso API.</param>
    public static IServiceCollection AddApiServices(this IServiceCollection services, string baseAddress)
    {
        services.AddSingleton<IShortenedUrlService, ShortenedUrlService>();

        services.AddSingleton<IApiClientFactory, ApiHttpClientFactory>();
        services.AddHttpClient(ApiHttpClientFactory.HttpClientName,
            client => client.BaseAddress = new Uri(baseAddress));

        return services;
    }
}
