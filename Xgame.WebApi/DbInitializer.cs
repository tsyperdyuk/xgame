using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xgame.Db;
using Xgame.Db.Entities;

namespace Xgame.WebApi
{
    public class DbInitializer
    {
        public static async Task TryInitDb(IWebHost webHost)
        {


            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<XgameContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var user = new User()
                        {
                            FirstName = "Andriy",
                            LastName = "T"
                        };
                    await userManager.CreateAsync(user, "123");
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
