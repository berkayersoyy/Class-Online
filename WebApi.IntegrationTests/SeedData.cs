
using System.Linq;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace WebApi.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(NorthwindContext _context)
        {
            _context.Videos.Add(new Video
            {
                Title = "Title 1",
                Extension = "mp4",
                Description = "Desc 1",
                Path = "videos"
            });
            _context.Videos.Add(new Video
            {
                Title = "Title 2",
                Extension = "wav",
                Description = "Desc 2",
                Path = "videos"
            });
            _context.Videos.Add(new Video
            {
                Title = "Title 3",
                Extension = "flv",
                Description = "Desc 3",
                Path = "videos"
            });
            _context.SaveChanges();
        }
    }
}