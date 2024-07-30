using TokenProject.IRepositories;

namespace TokenProject.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Save();
        Task CompleteAsync();
    }
}
