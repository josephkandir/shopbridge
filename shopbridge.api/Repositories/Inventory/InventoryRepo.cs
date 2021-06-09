using Microsoft.EntityFrameworkCore;
using shopbridge.api.Context;
using shopbridge.api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopbridge.api.Repositories
{
    public class InventoryRepo : IInventory
    {
        private readonly ApplicationContext _context;
        public InventoryRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Inventory> CreateAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAsync()
        {
            return await _context.Inventories.ToListAsync();
        }

        public async Task<Inventory> GetAsync(int id)
        {
            return await _context.Inventories.FindAsync(id);
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Entry(inventory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
