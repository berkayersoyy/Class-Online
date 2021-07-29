
using System.Linq;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace WebApi.IntegrationTests
{
    public static class SeedData
    {
        private static void VideoSeed(NorthwindContext _context)
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
        private static void PlaylistSeed(NorthwindContext _context)
        {
            _context.Playlists.Add(new Playlist
            {
                VideoList = "1,2"
            });
            _context.Playlists.Add(new Playlist
            {
                VideoList = "3,4"
            });
            _context.Playlists.Add(new Playlist
            {
                VideoList = "5,6,7"
            });
            _context.SaveChanges();
        }

        private static void UserSeed(NorthwindContext _context)
        {
            byte[] passwordHash1, passwordSalt1;
            HashingHelper.CreatePasswordHash("123456789",out passwordHash1,out passwordSalt1);
            _context.Users.Add(new User
            {
                FirstName = "berkay",
                LastName = "ersoy",
                Email = "sampleemail1@domain.com",
                Status = true,
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1
            });
            _context.Users.Add(new User
            {
                FirstName = "betul",
                LastName = "ersoy",
                Email = "sampleemail2@domain.com",
                Status = true,
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1
            });
            _context.Users.Add(new User
            {
                FirstName = "kamil",
                LastName = "ersoy",
                Email = "sampleemail3@domain.com",
                Status = true,
                PasswordHash = passwordHash1,
                PasswordSalt = passwordSalt1
            });
            _context.SaveChanges();
        }
        public static void PopulateTestData(NorthwindContext _context)
        {
            VideoSeed(_context);
            PlaylistSeed(_context);
            UserSeed(_context);
        }
    }
}