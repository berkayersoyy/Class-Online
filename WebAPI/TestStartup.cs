using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI
{
    public class TestStartup:Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>(options =>
                options.UseInMemoryDatabase("InMemoryTest"));
        }

    }
}