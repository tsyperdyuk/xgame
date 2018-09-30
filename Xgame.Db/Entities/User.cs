using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Xgame.Db.Models
{
    public class User : IdentityUser
    {
        public virtual int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Password { get; set; }
      
    }
}
