using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPlaylistService
    {
        IResult Add(Playlist playlist);
        IResult Delete(Playlist playlist);
        IResult Update(Playlist playlist);
        IDataResult<Playlist> Get(int id);
        IDataResult<List<Playlist>> GetList();
    }
}