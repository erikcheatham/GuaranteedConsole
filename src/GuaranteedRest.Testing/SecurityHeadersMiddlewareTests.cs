using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using NetEscapades.AspNetCore.SecurityHeaders.Infrastructure;
using Xunit;

namespace GuaranteedRest.Testing
{
    public class SecurityHeadersMiddlewareTests
    {
        [Fact]
        public async Task DefaultSecurePolicy_RemovesServerHeader()
        {
            // Arrange
            var policy = new SecurityHeadersPolicyBuilder()
                .AddDefaultSecurePolicy();

            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddSecurityHeaders())
                .Configure(app =>
                {
                    app.UseSecurityHeadersMiddleware(policy);
                    app.Run(async context =>
                    {
                        await context.Response.WriteAsync("Test response");
                    });
                });

            using (var server = new TestServer(hostBuilder))
            {
                // Act
                // Actual request.
                var response = await server.CreateRequest("/")
                    .SendAsync("GET");

                // Assert
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                Assert.Equal("Test response", content);
                Assert.False(response.Headers.Contains("Server"), "Should not contain server header");
            }
        }
    }
}