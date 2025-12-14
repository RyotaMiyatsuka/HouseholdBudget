using System.Net;
using System.Text.Json;

using HouseholdBudget.Defines.Exceptions;

namespace HouseholdBudget.Presentation.Middlewares;

/// <summary>
/// グローバル例外ハンドリングミドルウェア
/// キャッチされなかった例外を適切なHTTPレスポンスに変換する
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, response) = exception switch
        {
            NotFoundException ex => (
                HttpStatusCode.NotFound,
                new ErrorResponse("NotFound", ex.Message)
            ),
            ValidationException ex => (
                HttpStatusCode.BadRequest,
                new ErrorResponse("ValidationError", ex.Message, ex.Errors)
            ),
            ConflictException ex => (
                HttpStatusCode.Conflict,
                new ErrorResponse("Conflict", ex.Message)
            ),
            UnauthorizedException ex => (
                HttpStatusCode.Unauthorized,
                new ErrorResponse("Unauthorized", ex.Message)
            ),
            DomainException ex => (
                HttpStatusCode.InternalServerError,
                new ErrorResponse("DomainError", ex.Message)
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                new ErrorResponse("InternalServerError", "An unexpected error occurred.")
            )
        };

        _logger.LogError(
            exception, "Unhandled exception occurred: {ExceptionType} - {Message}",
            exception.GetType().Name,
            exception.Message
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}

/// <summary>
/// エラーレスポンスの形式
/// </summary>
public record ErrorResponse(
    string Code,
    string Message,
    IDictionary<string, string[]>? Errors = null
);

/// <summary>
/// ミドルウェア登録用の拡張メソッド
/// </summary>
public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
