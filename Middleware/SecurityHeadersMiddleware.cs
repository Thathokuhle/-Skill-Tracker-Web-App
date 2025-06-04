using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace SkillTrackerApp.Middleware
{
    public sealed class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly Dictionary<string, StringValues> _headers;

        public SecurityHeadersMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;

            _headers = new Dictionary<string, StringValues>(StringComparer.OrdinalIgnoreCase)
            {
                { "referrer-policy", "strict-origin-when-cross-origin" },
                { "x-content-type-options", "nosniff" },
                { "x-frame-options", "SAMEORIGIN" },
                { "X-Permitted-Cross-Domain-Policies", "none" }
                // Removed x-xss-protection (deprecated)
            };
        }

        public async Task Invoke(HttpContext context)
        {
            // Set standard headers
            foreach (var header in _headers)
            {
                context.Response.Headers[header.Key] = header.Value;
            }

            // Build CSP
            var csp = new StringBuilder()
                .Append("base-uri 'self';")
                .Append("block-all-mixed-content;")
                .Append("child-src 'self';")
                .Append("font-src 'self' https://fonts.googleapis.com https://fonts.gstatic.com https://cdnjs.cloudflare.com https://cdn.jsdelivr.net https://unpkg.com;")
                .Append("form-action 'self' https://accounts.google.com;")
                .Append("frame-ancestors 'none';")
                .Append("frame-src 'self' https://www.google.com https://www.google.com/maps;")
                .Append("img-src 'self' data: https://maps.googleapis.com https://maps.gstatic.com https://cdn.jsdelivr.net https://www.google-analytics.com;")
                .Append("manifest-src 'none';")
                .Append("media-src 'self';")
                .Append("object-src 'none';")
                .Append("script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.jsdelivr.net https://cdnjs.cloudflare.com https://code.jquery.com https://maps.googleapis.com https://unpkg.com https://cdn.tiny.cloud https://apis.google.com;")
                .Append("script-src-elem 'self' 'unsafe-inline' https://cdn.jsdelivr.net https://cdnjs.cloudflare.com https://code.jquery.com https://maps.googleapis.com https://unpkg.com https://cdn.tiny.cloud https://apis.google.com;")
                .Append("style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdn.jsdelivr.net https://cdnjs.cloudflare.com https://cdn.tiny.cloud;");

            // Add development-only sources for Browser Link, SignalR, etc.
            if (_env.IsDevelopment())
            {
                csp.Append("connect-src 'self' http://localhost:* ws://localhost:* wss://localhost:* https://maps.googleapis.com https://maps.gstatic.com;");
            }
            else
            {
                csp.Append("connect-src 'self' https://maps.googleapis.com https://maps.gstatic.com;");
            }

            context.Response.Headers["Content-Security-Policy"] = csp.ToString();

            await _next(context);
        }
    }
}
