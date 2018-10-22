using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xgame.Db.Entities;
using Xgame.Model;

namespace Xgame.Db
{
    public class XgameContext : IdentityDbContext<AppUser>
    {
        public XgameContext(DbContextOptions<XgameContext> option) : base(option)
        {            
        }        

        public  DbSet<Question> Questions { get; set; }

    }
}
