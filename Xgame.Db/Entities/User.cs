using System.ComponentModel.DataAnnotations;

namespace Xgame.Db.Entities
{
    public class User 
    {
        [Key]
        public virtual int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
