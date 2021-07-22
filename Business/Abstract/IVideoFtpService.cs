using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IVideoFtpService
    {
        IResult UploadToHost(Video video, string path);
        IResult DeleteFromHost(Video video, string path);
    }
}