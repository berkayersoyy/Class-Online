using Core.Entities.Abstract;

namespace Core.DataAccess.Ftp
{
    public interface IFtpRepository<T>
    where T:class,IFtp,new()
    {
        void Add(T entity,string path);
        void Delete(T entity);
    }
}