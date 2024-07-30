

using TokenProject.Entities;
using TokenProject.IRepositories;

namespace TokenProject.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
