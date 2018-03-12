using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTrackerBackend.Models;

namespace TimeTrackerBackend.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return await List();          
        }

        public async Task<User> GetByName(string userName)
        {
            return await FindAsync(x => x.UserName.Equals(userName));
        }
    }
}
