using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Globalization;

public class PreferredLanguageMiddleware
{
    private readonly RequestDelegate _next;

    public PreferredLanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Extract the preferred language from the Accept-Language header
        string acceptLanguageHeader = context.Request.Headers["Accept-Language"];

        if (!string.IsNullOrEmpty(acceptLanguageHeader))
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture(acceptLanguageHeader);
            string preferredLanguage = culture.Name;

            // Store the preferred language in a context item for access throughout the application
            context.Items["PreferredLanguage"] = preferredLanguage;
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}

public static class PreferredLanguageMiddlewareExtensions
{
    public static IApplicationBuilder UsePreferredLanguageMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PreferredLanguageMiddleware>();
    }
}
