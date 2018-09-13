using System.Collections.Generic;
using System.Linq;
using Xgame.Db.Entities;

namespace Xgame.Db
{
    public static class XgameExtensions
    {
        public static void EnsureSeedDataForContext(this XgameContext context)
        {
            if (!context.Users.Any())
            {


                var users = new List<User>()
            {
                new User()
                {
                    //Id = 1,
                    FirstName = "Andmin",
                    LastName = "Admin",
                },
               new User()
                {
                    //Id = 2,
                    FirstName = "Andriy",
                    LastName = "T",
                },
               new User()
                {
                    //Id = 3,
                    FirstName = "AAAA",
                    LastName = "BBBB",
                }
            };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
