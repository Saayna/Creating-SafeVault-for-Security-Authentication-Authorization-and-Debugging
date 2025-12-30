public static class SecurityHeadersExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (ctx, next) =>
        {
            ctx.Response.Headers.TryAdd("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            ctx.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
            ctx.Response.Headers.TryAdd("X-Frame-Options", "DENY");
            ctx.Response.Headers.TryAdd("Content-Security-Policy", "default-src 'self'");
            await next();
        });
    }
}
