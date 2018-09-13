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

        public DbSet<User> Users { get; set; }
    }
}
