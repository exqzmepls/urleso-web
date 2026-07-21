namespace Urleso.Web.Service.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddApiSettings(this IServiceCollection services)
    {
        services.AddOptions<ApiSettings>()
            .BindConfiguration(ApiSettings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
