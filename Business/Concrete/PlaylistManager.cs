using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PlaylistManager:IPlaylistService
    {
        private IPlaylistDal _playlistDal;

        public PlaylistManager(IPlaylistDal playlistDal)
        {
            _playlistDal = playlistDal;
        }

        public IResult Add(Playlist playlist)
        {
            _playlistDal.Add(playlist);
            return new SuccessResult();
        }

        public IResult Delete(Playlist playlist)
        {
            _playlistDal.Delete(playlist);
            return new SuccessResult();
        }

        public IResult Update(Playlist playlist)
        {
            _playlistDal.Update(playlist);
            return new SuccessResult();
        }

        public IDataResult<Playlist> Get(int id)
        {
            var result = _playlistDal.Get(x => x.Id == id);
            return new SuccessDataResult<Playlist>(result);
        }

        public IDataResult<List<Playlist>> GetList()
        {
            var result = _playlistDal.GetAll();
            return new SuccessDataResult<List<Playlist>>(result);
        }
    }
}