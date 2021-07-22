using System.Diagnostics;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using FluentFTP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule:ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager,MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
            serviceCollection.AddSingleton<FtpClient>();
        }
    }
}