using Prepa.Models.Group;
using Microsoft.EntityFrameworkCore;
using Prepa.Models;

namespace Prepa.Database
{
    public class DatabaseContext :DbContext
    {
        public DbSet<Group> Group { get; set; }
        public DbSet<Pupil> Pupils { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();

        }
    }
}
