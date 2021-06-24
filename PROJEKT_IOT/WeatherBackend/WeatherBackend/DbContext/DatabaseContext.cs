using Microsoft.EntityFrameworkCore;

namespace WeatherBackend.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public virtual DbSet<Weathers> Weathers { get; set; }

        
    }
}