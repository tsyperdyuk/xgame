using System;
using System.Collections.Generic;
using System.Text;
using Xgame.WebApi.Client.Providers.Interfaces;

namespace Xgame.WebApi.Client
{
    public interface IApiClient
    {
        IUserProvider User { get; }
    }
}
