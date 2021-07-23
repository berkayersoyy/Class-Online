using System;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=TrainerHome_{Guid.NewGuid()};Trusted_Connection=true"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app,env);
        }

    }
}