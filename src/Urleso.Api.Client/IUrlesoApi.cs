using Refit;

namespace Urleso.Api.Client;

public interface IUrlesoApi
{
    [Post("/api/shortened-urls")]
    Task<IApiResponse<ShortUrlResponse>> CreateShortUrlAsync(
        [Body] CreateShortUrlRequest request,
        CancellationToken cancellationToken
    );
}
