using Microsoft.EntityFrameworkCore;
using Xgame.Db.Entities;

namespace Xgame.Db
{
    public class XgameContext : DbContext
    {
        public XgameContext(DbContextOptions<XgameContext> option) : base(option)
        {
            // Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    FirstName = "Andmin",
                    LastName = "Admin",
                },
                new User()
                {
                    UserId = 2,
                    FirstName = "Andriy",
                    LastName = "T",
                },
                new User()
                {
                    UserId = 3,
                    FirstName = "AAAA",
                    LastName = "BBBB",
                }
            );
        }

        public DbSet<User> Users { get; set; }
    }
}
