using Core.DataAccess.Ftp;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentFTP;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.FluentFtp
{
    public class FtpVideoDal:FtpRepositoryBase<Video,FtpClient>,IFtpVideoDal
    {
    }
}