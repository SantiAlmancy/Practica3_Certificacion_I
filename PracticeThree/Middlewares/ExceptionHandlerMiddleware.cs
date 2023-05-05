using System.Globalization;
using Serilog;

namespace UPB.PracticeThree.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.ILogger _logs;

    public ExceptionHandlerMiddleware(RequestDelegate next, Serilog.ILogger logs)
    {
        _next = next;
        _logs = logs;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline.
            _logs.Information("Request success");
            await _next(context);
        }
        catch (System.Exception ex)
        {
            // Log ex.Message
            _logs.Error("Failed Request: " + ex.Message);
            HandleException(context, ex);
        }

    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "text/json";
        // context.Response.StatusCode = 500;
        return context.Response.WriteAsync("AN ERROR OCCURRED: " + ex.Message);
    }
}

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app, Serilog.ILogger logger)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>(logger);
    }
}