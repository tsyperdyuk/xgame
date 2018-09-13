using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xgame.Model;

namespace Xgame.Core
{
    public class UserManager
    {
        public async Task<List<UserModel>> GetUsersAsync()
        {
            var result = new List<UserModel>
            {
                new UserModel{Name = "Adnriushenka"},
                new UserModel{Name = "Seriogenka"}
            };
            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
