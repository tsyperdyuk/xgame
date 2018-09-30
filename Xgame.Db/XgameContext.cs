using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xgame.Db.Models;

namespace Xgame.Db
{
    public class XgameContext : IdentityDbContext<User>
    {
        public XgameContext(DbContextOptions<XgameContext> option) : base(option)
        {
            
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
