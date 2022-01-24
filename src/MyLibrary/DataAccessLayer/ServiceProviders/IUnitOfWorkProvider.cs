//MIT License

namespace MyLibrary.DataAccessLayer.ServiceProviders
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork Get();
    }
}