using ChainSaw.Server.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ChainSaw.Server.Data
{
    public class ChainSawDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ChainSawServer.db");
        }
    }
}
