using System.Net;

namespace RevenueRecognitionSystem.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next middleware in the pipeline
            await next(context);
        }
        catch (Exception ex)
        {
            // Log the exception
            logger.LogError(ex, "An unhandled exception occurred");

            // Handle the exception
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Set the status code and response content
        context.Response.StatusCode = (int)GetStatusCodeFromException(exception);
        context.Response.ContentType = "application/json";

        // Create a response model
        var response = new
        {
            error = new
            {
                message = "An error occurred while processing your request.",
                detail = exception.Message
            }
        };

        // Serialize the response model to JSON
        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        // Write the JSON response to the HTTP response
        return context.Response.WriteAsync(jsonResponse);
    }
    
    private HttpStatusCode GetStatusCodeFromException(Exception exception)
    {
        switch (exception)
        {
            case ArgumentNullException _:
            case ArgumentException _:
                return HttpStatusCode.BadRequest;

            case UnauthorizedAccessException _:
                return HttpStatusCode.Unauthorized;

            case KeyNotFoundException _:
                return HttpStatusCode.NotFound;

            case InvalidOperationException _:
                return HttpStatusCode.Conflict;

            default:
                return HttpStatusCode.InternalServerError;
        }
    }
}