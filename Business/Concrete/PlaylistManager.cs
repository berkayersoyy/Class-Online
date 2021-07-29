using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
        [ValidationAspect(typeof(PlaylistValidator))]
        public IResult Add(Playlist playlist)
        {
            _playlistDal.Add(playlist);
            return new SuccessResult(Messages.PlaylistAdded);
        }

        public IResult Delete(Playlist playlist)
        {
            _playlistDal.Delete(playlist);
            return new SuccessResult(Messages.PlaylistDeleted);
        }
        [ValidationAspect(typeof(PlaylistValidator))]
        public IResult Update(Playlist playlist)
        {
            _playlistDal.Update(playlist);
            return new SuccessResult(Messages.PlaylistUpdated);
        }

        public IDataResult<Playlist> Get(int id)
        {
            var result = _playlistDal.Get(x => x.Id == id);
            return new SuccessDataResult<Playlist>(result,Messages.GetPlaylist);
        }

        public IDataResult<List<Playlist>> GetList()
        {
            var result = _playlistDal.GetAll();
            return new SuccessDataResult<List<Playlist>>(result, Messages.GetPlaylists);
        }
    }
}