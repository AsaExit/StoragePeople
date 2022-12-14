using Microsoft.EntityFrameworkCore;
using StoragePeople.Models.Enterties;

namespace StoragePeople.Data
{
    public class PeopleDbContext: DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options): base(options)
        {
        }
        public DbSet<Person>? People { get; set; } = default!;
        public DbSet<City>? Cities { get; set; } = default!;
        public DbSet<Country>? Countries { get; set; } = default!;
        public DbSet<Language> Languages { get; set; }
    }
}
