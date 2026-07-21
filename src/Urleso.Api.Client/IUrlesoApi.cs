using Refit;

namespace Urleso.Api.Client;

/// <summary>
/// The Urleso API surface, hand-written against <see href="swagger.json">the vendored OpenAPI document</see>.
/// Keep the two in sync by hand — nothing verifies this interface against the document at build time.
/// </summary>
public interface IUrlesoApi
{
    /// <returns>
    /// A non-throwing response: 400 carries an <see cref="ErrorDetails"/> body,
    /// readable via <c>response.Error.GetContentAsAsync&lt;ErrorDetails&gt;()</c>.
    /// </returns>
    [Post("/api/shortened-urls")]
    Task<IApiResponse<ShortenedUrlDetails>> ShortenUrlAsync(
        [Body] ShortenedUrlOptions options,
        CancellationToken cancellationToken = default
    );
}
