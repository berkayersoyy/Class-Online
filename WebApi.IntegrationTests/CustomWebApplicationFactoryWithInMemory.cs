using System;
using System.Linq;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static WebApi.IntegrationTests.SeedData;

namespace WebApi.IntegrationTests
{
    public class CustomWebApplicationFactoryWithInMemory<TStartup> :BaseWebApplicationFactory<TStartup> where TStartup : class
    {

         protected override void ConfigureWebHost(IWebHostBuilder builder)
         {
             builder.ConfigureServices(services =>
             {
                 var descriptor =
                     services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<NorthwindContext>));
                 if (descriptor != null)
                 {
                     services.Remove(descriptor);
                 }

                 var serviceProvider =
                     new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                 services.AddDbContext<NorthwindContext>(options =>
                 {
                     options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                     options.UseInternalServiceProvider(serviceProvider);
                 });

                 var sp = services.BuildServiceProvider();

                 using (var scope = sp.CreateScope())
                 {
                     var scopedServices = scope.ServiceProvider;
                     var appDb = scopedServices.GetRequiredService<NorthwindContext>();

                     appDb.Database.EnsureCreated();

                     try
                     {
                         PopulateTestData(appDb);
                     }
                     catch (Exception e)
                     {
                         Console.WriteLine(e);
                         throw;
                     }
                 }
             });
         }

    }
}