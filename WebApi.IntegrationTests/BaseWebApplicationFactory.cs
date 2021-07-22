
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.IntegrationTests
{
    public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
    }
}