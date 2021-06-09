using shopbridge.api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopbridge.api.Repositories
{
    public interface IInventory
    {
        Task<IEnumerable<Inventory>> GetAsync();

        Task<Inventory> GetAsync(int id);

        Task<Inventory> CreateAsync(Inventory inventory);

        Task UpdateAsync(Inventory inventory);

        Task DeleteAsync(int id);
    }
}
