using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopbridge.api.Models;

namespace shopbridge.api.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
