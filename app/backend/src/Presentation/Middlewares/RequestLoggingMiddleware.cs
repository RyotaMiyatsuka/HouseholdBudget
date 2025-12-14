using System.Diagnostics;

namespace HouseholdBudget.Presentation.Middlewares;

/// <summary>
/// リクエストログ出力ミドルウェア
/// リクエストの開始・完了時にログを出力する
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var method = context.Request.Method;
        var path = context.Request.Path;
        var queryString = context.Request.QueryString;

        _logger.LogInformation(
            "Request started: {Method} {Path}{QueryString}",
            method, path, queryString
        );

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;
            var elapsed = stopwatch.ElapsedMilliseconds;

            if (statusCode >= 500)
            {
                _logger.LogError(
                    "Request completed: {Method} {Path} - {StatusCode} in {Elapsed}ms",
                    method, path, statusCode, elapsed
                );
            }
            else if (statusCode >= 400)
            {
                _logger.LogWarning(
                    "Request completed: {Method} {Path} - {StatusCode} in {Elapsed}ms",
                    method, path, statusCode, elapsed
                );
            }
            else
            {
                _logger.LogInformation(
                    "Request completed: {Method} {Path} - {StatusCode} in {Elapsed}ms",
                    method, path, statusCode, elapsed
                );
            }
        }
    }
}

/// <summary>
/// ミドルウェア登録用の拡張メソッド
/// </summary>
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
