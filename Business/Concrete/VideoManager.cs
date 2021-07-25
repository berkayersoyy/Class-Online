
using System.Collections.Generic;
using System.IO;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Ftp;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class VideoManager : IVideoService
    {
        private IVideoDal _videoDal;

        public VideoManager(IVideoDal videoDal)
        {
            _videoDal = videoDal;
        }


        public IDataResult<List<Video>> GetList()
        {
            return new SuccessDataResult<List<Video>>(_videoDal.GetAll(), Messages.GetAllVideos);
        }
         
        public IDataResult<Video> Get(int id)
        {
            return new SuccessDataResult<Video>(_videoDal.Get(v => v.Id == id), Messages.GetVideo);
        }
        [ValidationAspect(typeof(VideoValidator))]
        public IResult Add(Video video)
        {
            _videoDal.Add(video);
            return new SuccessResult(Messages.VideoAdded);
        }

        public IResult Delete(Video video)
        {
            _videoDal.Delete(video);
            return new SuccessResult(Messages.VideoDeleted);
        }

        public IResult Update(Video video)
        {
            _videoDal.Update(video);
            return new SuccessResult(Messages.VideoUpdated);
        }

    }
}