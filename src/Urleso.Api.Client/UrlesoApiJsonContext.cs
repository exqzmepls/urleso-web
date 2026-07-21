using System.Text.Json.Serialization;

namespace Urleso.Api.Client;

/// <summary>
/// Source-generated metadata for the API contracts: keeps them serializable under the WASM
/// publish trimmer, which strips the reflection the default serializer would otherwise need.
/// </summary>
/// <remarks>
/// The naming policy is load-bearing: the API's wire contract is PascalCase (<c>LongUrl</c>,
/// <c>UrlCode</c>), so property names must go out exactly as declared. A camelCase policy —
/// what <see cref="System.Text.Json.JsonSerializerDefaults.Web"/>, and therefore Refit's own
/// default, would apply — silently breaks request bodies.
/// </remarks>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
[JsonSerializable(typeof(ShortenedUrlOptions))]
[JsonSerializable(typeof(ShortenedUrlDetails))]
[JsonSerializable(typeof(ErrorDetails))]
internal sealed partial class UrlesoApiJsonContext : JsonSerializerContext;
