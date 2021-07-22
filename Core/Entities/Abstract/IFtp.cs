namespace Core.Entities.Abstract
{
    public interface IFtp
    {
        int Id { get; }
        string Path { get; }
        string Extension { get; }
    }
}