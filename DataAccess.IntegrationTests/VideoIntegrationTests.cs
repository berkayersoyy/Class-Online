using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DataAccess.IntegrationTests
{
    public class VideoIntegrationTests:IDisposable
    {
        private NorthwindContext _context;
        public VideoIntegrationTests()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();
            var options = new DbContextOptionsBuilder<NorthwindContext>();
            options.UseSqlServer(
                $"Server=(localdb)\\mssqllocaldb;Database=TrainerHome_{Guid.NewGuid().ToString()};Trusted_Connection=True;MultipleActiveResultSets=true").UseInternalServiceProvider(serviceProvider);
            _context = new NorthwindContext(options.Options);
            _context.Database.EnsureCreated();
        }
        [Fact]
        public void Add_WithValidData_ShouldReturnSuccess()
        {
            Seed(_context);
            var actual = _context.Videos.ToList();
            var expected = SampleVideos().Count;
            Assert.True(actual!=null);
            Assert.Equal(expected,actual.Count);
        }
        private List<Video> SampleVideos()
        {
            List<Video> videos = new List<Video>
            {
                new Video
                {
                    Path = "videos",
                    Description = "video",
                    Extension = "mp4",
                    Title = "Video 1"
                },
                new Video
                {
                    Path = "videos",
                    Description = "video",
                    Extension = "mp4",
                    Title = "Video 1"
                },
                new Video
                {
                    Path = "videos",
                    Description = "video",
                    Extension = "mp4",
                    Title = "Video 1"
                },
                new Video
                {
                    Path = "videos",
                    Description = "video",
                    Extension = "mp4",
                    Title = "Video 1"
                },
                new Video
                {
                    Path = "videos",
                    Description = "video",
                    Extension = "mp4",
                    Title = "Video 1"
                }
            };
            return videos;
        }
        private void Seed(NorthwindContext context)
        {
            context.AddRange(SampleVideos());
            context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
