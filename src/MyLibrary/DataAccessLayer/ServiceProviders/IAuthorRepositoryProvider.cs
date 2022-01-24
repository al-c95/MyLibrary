//MIT License

using MyLibrary.DataAccessLayer.Repositories;

namespace MyLibrary.DataAccessLayer.ServiceProviders
{
    public interface IAuthorRepositoryProvider
    {
        IAuthorRepository Get(IUnitOfWork uow);
    }
}