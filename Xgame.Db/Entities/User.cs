using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Xgame.Db.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public virtual int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int IsAdmin { get; set; }
    }
}
