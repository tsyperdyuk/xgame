using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xgame.Db.Entities;

namespace Xgame.Db
{
    public class XgameContext : IdentityDbContext<User>
    {
        public XgameContext(DbContextOptions<XgameContext> option) : base(option)
        {
            
        }
    }
}
