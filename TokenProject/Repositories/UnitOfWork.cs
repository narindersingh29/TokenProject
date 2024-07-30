

using TokenProject.IRepositories;

namespace TokenProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;


        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            Users = userRepository;
        }

        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public Task CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
