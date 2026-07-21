using FluentResults;
using Refit;
using Urleso.Api.Client;

namespace Urleso.Web.Api.ShortenedUrls;

internal sealed class ShortenedUrlService(
        IUrlesoApi api,
        ILogger<ShortenedUrlService> logger
    )
    : IShortenedUrlService
{
    public async Task<Result<string>> ShortenUrlAsync(string longUrl, CancellationToken cancellationToken = default)
    {
        var shortenedUrlOptions = new ShortenedUrlOptions(longUrl);

        try
        {
            using var response = await api.ShortenUrlAsync(shortenedUrlOptions, cancellationToken);
            return response.IsSuccessStatusCode
                ? response.Content!.Url
                : FailFromApiError(response.Error);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unexpected error: {@ApiUnexpectedError}", exception);
            return Result.Fail<string>("Something unexpected happened, ops...");
        }
    }

    /// <param name="error">
    /// Only <see cref="ApiException"/> carries a response; an <see cref="ApiRequestException"/> means the
    /// request never got one, so it is reported like any other unexpected failure.
    /// </param>
    private Result<string> FailFromApiError(ApiExceptionBase? error)
    {
        if (error is not ApiException apiException)
        {
            logger.LogError(error, "API request failed without a response: {@ApiUnexpectedError}", error);
            return Result.Fail<string>("Something unexpected happened, ops...");
        }

        // Only the API's own 400 carries ErrorDetails, and a body missing its members deserializes to
        // nulls rather than failing, so an empty Description means "not the error contract" too.
        if (apiException.TryGetContentAs<ErrorDetails>(out var errorDetails)
            && errorDetails is not null
            && !string.IsNullOrWhiteSpace(errorDetails.Description))
        {
            logger.LogError(apiException, "API request error details: {@ErrorDetails}", errorDetails);
            return Result.Fail<string>(errorDetails.Description);
        }

        logger.LogError(apiException,
            "API error ({ApiErrorStatusCode}) with response: '{ApiErrorResponse}' and headers: '{@ApiErrorHeaders}'",
            apiException.StatusCode, apiException.Content, apiException.Headers
        );
        return Result.Fail<string>("Some error occurred :( please try again.");
    }
}
