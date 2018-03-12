using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTrackerBackend.Interfaces;
using TimeTrackerBackend.Models;

namespace TimeTrackerBackend.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IList<User>> GetUsersAsync();
        Task<User> GetByName(string userName);
    }
}
