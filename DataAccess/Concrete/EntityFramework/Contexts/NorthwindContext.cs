using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {

        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {
 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TrainerHome_Test;Trusted_Connection=true");
            }
        }


        public DbSet<UserPlaylist> UserPlaylists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
