using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPlaylistDal:EfEntityRepositoryBase<Playlist,NorthwindContext>,IPlaylistDal
    {

        public List<Playlist> GetUserPlaylists(int userId)
        {
            using (var context = new NorthwindContext())
            {
                var result = from playlist in context.Playlists
                    join userPlaylist in context.UserPlaylists on playlist.Id equals userPlaylist.PlaylistId
                    where userPlaylist.PlaylistId == userId
                    select new Playlist
                    {
                        Id = playlist.Id,
                        VideoList = playlist.VideoList
                    };
                return result.ToList();
            }
        }
    }
}