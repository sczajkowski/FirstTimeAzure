using Microsoft.EntityFrameworkCore;
using FirstTimeAzure.Models;

namespace FirstTimeAzure
{
    public class PeopleDb : DbContext
    {
        public PeopleDb(DbContextOptions<PeopleDb> options)
        : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
