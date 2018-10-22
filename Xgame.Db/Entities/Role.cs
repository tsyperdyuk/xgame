using System;
using System.Collections.Generic;
using System.Text;

namespace Xgame.Db.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppUser> Users { get; set; }
        public Role()
        {
            Users = new List<AppUser>();
        }
    }
}
