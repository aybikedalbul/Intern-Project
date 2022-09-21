using Microsoft.EntityFrameworkCore;

namespace InternProject.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
    }
}
