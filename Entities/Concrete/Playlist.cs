using System.Collections.Generic;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Playlist:IEntity
    {
        public int Id { get; set; }
        public string VideoList { get; set; }

    }
}