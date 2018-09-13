using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xgame.Model;

namespace Xgame.WebApi.Client.Providers.Interfaces
{
    public interface IUserProvider
    {
        Task<List<UserModel>> GetAllAsync();
    }
}
