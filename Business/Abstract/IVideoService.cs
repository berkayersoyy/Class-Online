using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IVideoService
    {
        IDataResult<List<Video>> GetList();
        IDataResult<Video> Get(int id);
        IResult Add(Video video);
        IResult Delete(Video video);
        IResult Update(Video video);
    }
}