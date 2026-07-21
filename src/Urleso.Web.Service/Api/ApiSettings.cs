using System.ComponentModel.DataAnnotations;

namespace Urleso.Web.Service.Api;

internal sealed class ApiSettings
{
    public const string ConfigurationSection = "Api";

    [Required]
    [Url]
    public required string BaseAddress { get; init; }
}
