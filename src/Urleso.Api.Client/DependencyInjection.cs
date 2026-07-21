using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Urleso.Api.Client;

public static class DependencyInjection
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        TypeInfoResolver = UrlesoApiJsonContext.Default,
        PropertyNamingPolicy = null
    };

    /// <param name="baseAddress">The origin to resolve the API's paths against.</param>
    public static IServiceCollection AddUrlesoApiClient(this IServiceCollection services, Uri baseAddress)
    {
        var settings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(SerializerOptions)
        };

        services.AddRefitClient<IUrlesoApi>(settings)
            .ConfigureHttpClient(client => client.BaseAddress = baseAddress);

        return services;
    }
}
