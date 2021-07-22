using System.IO;
using Business.Abstract;
using Business.Constants;
using Core.DataAccess.Ftp;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class VideoFtpManager:IVideoFtpService
    {
        private IFtpVideoDal _ftpVideo;

        public VideoFtpManager(IFtpVideoDal ftpVideo)
        {
            _ftpVideo = ftpVideo;
        }

        public IResult UploadToHost(Video video, string path)
        {
            var result = BusinessRules.Run(CheckIfVideo(path));
            if (result != null)
            {
                return new ErrorResult(Messages.NotAVideo);
            }
            _ftpVideo.Add(video, path);
            return new SuccessResult(Messages.VideoUploadedToHost);
        }

        public IResult DeleteFromHost(Video video, string path)
        {
            var result = BusinessRules.Run(CheckIfVideo(path));
            if (result != null)
            {
                return new ErrorResult(Messages.NotAVideo);
            }
            _ftpVideo.Delete(video);
            return new SuccessResult(Messages.VideoDeletedFromHost);
        }

        private IResult CheckIfVideo(string path)
        {
            foreach (var extension in MediaOptions.MediaExtensions)
            {
                if (path.Contains(extension))
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult();
        }

        private string GetExtension(string path)
        {
            var stream = File.Open(path, FileMode.Open);
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Extension;
        }
    }
}