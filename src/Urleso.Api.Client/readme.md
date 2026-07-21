# Urleso.Api.Client

C# client for the Urleso API, hand-written with [Refit](https://github.com/reactiveui/refit) against the vendored OpenAPI document ([`swagger.json`](swagger.json)).

`swagger.json` is kept here as the record of the API's contract, but nothing generates code from it and nothing checks the client against it. **After an API change the interface and contracts below must be updated by hand.**

## Layout

- [`IUrlesoApi.cs`](IUrlesoApi.cs) — the API surface, one Refit-annotated method per operation
- `ShortenedUrlOptions` / `ShortenedUrlDetails` / `ErrorDetails` — the request and response contracts
- [`UrlesoApiJsonContext.cs`](UrlesoApiJsonContext.cs) — source-generated `System.Text.Json` metadata for those contracts
- [`DependencyInjection.cs`](DependencyInjection.cs) — `AddUrlesoApiClient(baseAddress)`, which registers the Refit client and its serializer settings

## How to update the client after an API change

1. In the [Urleso repository](https://github.com/exqzmepls/Urleso), regenerate the OpenAPI document:

    ```shell
    dotnet build src/Urleso.Api -c Release -p:OpenApiGen=True
    ```

2. Copy the resulting `src/Urleso.Api/swagger.json` into this directory (replacing the existing `swagger.json`)
3. Update `IUrlesoApi` and the contract records to match, and register any new contract type in `UrlesoApiJsonContext` — a type missing from the context fails to serialize once the WASM trimmer runs, which a `dotnet run` will not reveal

## The PascalCase contract

The API's wire format is PascalCase (`LongUrl`, `Url`, `UrlCode`), whereas Refit's default serializer settings are `JsonSerializerDefaults.Web` — camelCase. `AddUrlesoApiClient` therefore supplies its own `JsonSerializerOptions` with `PropertyNamingPolicy = null` so property names go out exactly as declared. Dropping that would silently break request bodies rather than fail loudly, so keep new contracts free of `[JsonPropertyName]` overrides and let the declared names carry the contract.
