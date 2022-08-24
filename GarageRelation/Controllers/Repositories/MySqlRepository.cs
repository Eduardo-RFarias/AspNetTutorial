using GarageRelation.Configuration;
using GarageRelation.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageRelation.Controllers.Repositories
{
    public sealed class MySqlRepository : DbContext
    {
        public DbSet<Person> Person { get; set; } = default!;
        public DbSet<Car> Car { get; set; } = default!;

        public MySqlRepository(DbContextOptions<MySqlRepository> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PersonConfiguration().Configure(modelBuilder.Entity<Person>());
            new CarConfiguration().Configure(modelBuilder.Entity<Car>());
        }
    }
}
