using System;
using System.Linq;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.IntegrationTests
{
    public class CustomWebApplicationFactoryWithSql<TStartup> :BaseWebApplicationFactory<TStartup> where TStartup : class
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
                     new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();

                 services.AddDbContext<NorthwindContext>(options =>
                 {
                     options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=TrainerHome_Test;Trusted_Connection=true");
                     options.UseInternalServiceProvider(serviceProvider);
                 });

                 var sp = services.BuildServiceProvider();

                 using (var scope = sp.CreateScope())
                 {
                     var scopedServices = scope.ServiceProvider;
                     var appDb = scopedServices.GetRequiredService<NorthwindContext>();

                     appDb.Database.EnsureDeleted();
                     appDb.Database.EnsureCreated();

                     try
                     {
                         SeedData.PopulateTestData(appDb);
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