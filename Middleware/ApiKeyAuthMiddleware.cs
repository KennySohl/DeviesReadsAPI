namespace DeviesReadsAPI.Middleware;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "X-API-Key";

    public ApiKeyAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        // Skip authentication for Swagger UI and OpenAPI endpoints
        if (context.Request.Path.StartsWithSegments("/swagger") || 
            context.Request.Path.StartsWithSegments("/api-docs"))
        {
            await _next(context);
            return;
        }

        // Check if API key header exists
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        // Get valid API key from configuration
        var apiKey = configuration.GetValue<string>("ApiKey");

        // Validate API key
        if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(extractedApiKey.ToString()))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}
