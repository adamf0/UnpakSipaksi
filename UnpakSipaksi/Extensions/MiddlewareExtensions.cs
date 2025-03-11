using UnpakSipaksi.Middleware;

namespace UnpakSipaksi.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseUserAgentMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserAgentMiddleware>();
        }

        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}
