using shopbridge.api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopbridge.api.Repositories
{
    public interface IUser
    {
        Task<IEnumerable<Models.User>> GetAsync();

        Models.User GetAsync(int id);

        Models.User GetByUsernamePasswordAsync(string username, string password);
    }
}
