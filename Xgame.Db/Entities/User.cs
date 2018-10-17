using Microsoft.AspNetCore.Identity;

namespace Xgame.Db.Entities
{
    public class User : IdentityUser
    {
        public virtual int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
      
    }
}
