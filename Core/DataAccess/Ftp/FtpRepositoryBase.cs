
using System.Net;
using Core.Entities.Abstract;
using FluentFTP;
using Microsoft.Extensions.Configuration;

namespace Core.DataAccess.Ftp
{
    public class FtpRepositoryBase<TEntity, TFtp> : IFtpRepository<TEntity>
    where TEntity : class, IFtp, new()
    where TFtp : class, IFtpClient, new()
    {
        private TFtp _ftpClient;
        //TODO checkout 2 parameters
        public async void Add(TEntity entity, string path)
        {
            using (_ftpClient = new TFtp())
            {
                Setup();
                await _ftpClient.UploadFileAsync(path, $"{FtpOptions.HostName}/videos/{entity.Id}.{entity.Extension}");
            }

        }

        public async void Delete(TEntity entity)
        {
            using (_ftpClient = new TFtp())
            {
                Setup();
                await _ftpClient.DeleteFileAsync($"{FtpOptions.HostName}/videos/{entity.Id}.{entity.Extension}");
            }
        }

        private void Setup()
        {
            _ftpClient.Host = FtpOptions.HostName;
            _ftpClient.Credentials = new NetworkCredential(FtpOptions.Username, FtpOptions.Password);
        }
    }
}