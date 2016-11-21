using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Xunit;
using System.Net;
using Microsoft.AspNetCore.Http;
using GuaranteedRest.Testing;

namespace GuaranteedRest.Testing
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder()
              .AddDefaultSecurePolicy()
              .AddCustomHeader("X-My-Custom-Header", "So cool"));
        }

        public class TestFixture<TStartup> : IDisposable where TStartup : class
        {
            private readonly TestServer _server;

            public TestFixture()
            {
                var builder = new WebHostBuilder().UseStartup<TStartup>();
                _server = new TestServer(builder);

                Client = _server.CreateClient();
                Client.BaseAddress = new Uri("http://localhost:5000");
            }

            public HttpClient Client { get; }

            public void Dispose()
            {
                Client.Dispose();
                _server.Dispose();
            }
        }

        public class MiddlewareIntegrationTests : IClassFixture<TestFixture<GuaranteedRest.Testing.Startup>>
        {
            public MiddlewareIntegrationTests(TestFixture<GuaranteedRest.Testing.Startup> fixture)
            {
                Client = fixture.Client;
            }

            public HttpClient Client { get; }

            [Theory]
            [InlineData("GET")]
            [InlineData("HEAD")]
            [InlineData("POST")]
            public async Task AllMethods_RemovesServerHeader(string method)
            {
                // Arrange
                var request = new HttpRequestMessage(new HttpMethod("GET"), "/");

                // Act
                var response = await Client.SendAsync(request);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();

                Assert.Equal("Test response", content);
                Assert.False(response.Headers.Contains("Server"), "Should not contain server header");
            }
        }
    }
}
