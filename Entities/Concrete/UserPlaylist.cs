using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class UserPlaylist:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaylistId { get; set; }
    }
}