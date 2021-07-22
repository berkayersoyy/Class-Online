using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Video:IFtp, IEntity
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
    }
}