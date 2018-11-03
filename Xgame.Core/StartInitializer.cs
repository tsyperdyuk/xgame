using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xgame.Db.Entities;

namespace Xgame.Core
{
    public class StartInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StartInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync(Dictionary<string, string> users)
        {
            if(users.Count != 0)
            {
                foreach (KeyValuePair<string, string> entry in users)
                {
                    if (await _roleManager.FindByNameAsync(entry.Value) == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(entry.Value));
                    }
                    if (await _userManager.FindByNameAsync(entry.Key) == null)
                    {
                        AppUser admin = new AppUser { UserName = entry.Key };
                        IdentityResult result = await _userManager.CreateAsync(admin);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(admin, entry.Value);
                        }
                    }
                }
            }           
        }

    }
}
