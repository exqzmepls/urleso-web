using System.Text.Json.Serialization;

namespace Urleso.Api.Client;

/// <summary>
/// Source-generated metadata for the API contracts: keeps them serializable under the WASM
/// publish trimmer, which strips the reflection the default serializer would otherwise need.
/// </summary>
/// <remarks>
/// The naming policy is load-bearing: the API is a stock ASP.NET Core service, so it emits
/// camelCase (<c>url</c>, <c>urlCode</c>) regardless of how its DTOs are declared. Property-name
/// matching is case-sensitive, so a PascalCase policy does not fail loudly — the response
/// deserializes into an object whose every member is null.
/// </remarks>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(CreateShortUrlRequest))]
[JsonSerializable(typeof(ShortUrlResponse))]
internal sealed partial class UrlesoApiJsonContext : JsonSerializerContext;
